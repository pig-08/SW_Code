using CIW.Code.Player.Field;
using PSB.Code.CoreSystem.Events;
using PSW.Code.EventBus;
using PSW.Code.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Work.PSB.Code.FieldCode.MapSaves;
using Work.PSB.Code.FieldCode.MiniGames.MonsterHunt;

namespace PSW.Code.Talk
{
    public enum TalkEnableMode
    {
        Always,  //항상 대화 ㄱㄴ
        AfterEnemyDead  //적 잡고 ㄱㄴ
    }
        
    public enum InteractActionMode
    {
        TalkAndReward,  //대화하고 보상
        RewardOnly  //대화 없이 보상
    }

    [Serializable]
    public class TalkStage
    {
        [Header("Talk & Reward")]
        public TalkDataListSO talkData;
        public InteractActionMode actionMode = InteractActionMode.TalkAndReward;
        public string rewardKey;

        [Header("Unlock Condition")]
        public TalkEnableMode enableMode = TalkEnableMode.Always;
        public string targetEnemyId;

        [Header("Settings")]
        public bool isRepeatable = true;
    }
    
    public interface IInteractAction
    {
        void Setup(InteractContext ctx);
        bool CanExecute();
        void Execute();
    }
    
    public class TalkEntity : MonoBehaviour
    {
        [Header("TalkValueData")]
        [SerializeField] private string talkId;
        public string TalkId => talkId;
        
        [SerializeField] private Talk_Controller controller;
        [SerializeField] private PlayerFieldInputSO fieldInput;

        [Header("Talk Stages")]
        [SerializeField] private List<TalkStage> talkStages = new List<TalkStage>();

        [Header("After Finished")]
        [SerializeField] private bool disableTalkAfterFinished = false;
        [SerializeField] private bool disableObjectAfterFinished = false;
        [SerializeField] private float disableDelay = 0f;

        [Header("MapData")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float sensingRange = 1.5f;
        [SerializeField] private Color rangeColor = Color.green;

        public bool DisableObjectAfterFinished => disableObjectAfterFinished;
        public bool DisableTalkAfterFinished => disableTalkAfterFinished;
        
        public bool IsMiniGame => !string.IsNullOrEmpty(talkId) && talkId.StartsWith("MINIGAME_");
        
        private bool _isActuallyFinished = false;
        public bool IsFinished 
        { 
            get => _isActuallyFinished; 
            set 
            {
                if (talkStages == null || talkStages.Count <= 1 || _currentStageIndex >= talkStages.Count - 1)
                {
                    _isActuallyFinished = value; 
                }
            } 
        }

        private TalkButtonPanel _talkButtonPanel;

        private bool _isOnPlayer;
        private bool _isDefault;
        private bool _isStartTalk;
        private bool _isTalkEnabled;
        private bool _isForceDisabled;

        protected IInteractAction Action;
        
        private int _currentStageIndex = 0;
        private bool _isCurrentStagePlayed = false;

        public string RewardKey 
        {
            get 
            {
                if (talkStages == null || talkStages.Count == 0) return string.Empty;
                return talkStages[Mathf.Clamp(_currentStageIndex, 0, talkStages.Count - 1)].rewardKey;
            }
        }

        protected virtual void Awake()
        {
            SceneObjectRegistry.RegisterTalk(this);
        }

        protected void Start()
        {
            _talkButtonPanel = GetComponentInChildren<TalkButtonPanel>();
            _talkButtonPanel.Init();

            if (controller != null)
                controller.gameObject.SetActive(false);

            fieldInput.OnInteractPressed += TryInteract;
            
            Action = GetComponent<IInteractAction>();
            if (Action == null)
            {
                Action = gameObject.AddComponent<TalkRewardAction>();
            }
            
            SyncStageWithSaveData();
            RefreshEnableState();
        }

        private void OnDestroy()
        {
            fieldInput.OnInteractPressed -= TryInteract;
            SceneObjectRegistry.UnregisterTalk(this);
        }

        private void Update()
        {
            ChangePlayer();
            
            if (!_isStartTalk && talkStages.Count > 0)
            {
                TryAdvanceToNextStage();
            }
        }

        public void SetTalkEnabled(bool isEnabled)
        {
            SetStartTalkFlag(!isEnabled);
            _isTalkEnabled = isEnabled;
            _isForceDisabled = false;
            
            if (isEnabled)
            {
                _isDefault = false;
                _isOnPlayer = false;
            }
        }

        private void SyncStageWithSaveData()
        {
            if (talkStages == null || talkStages.Count == 0) return;

            if (!IsMiniGame)
            {
                string sceneName = SceneManager.GetActiveScene().name;
                var state = SceneSaveSystem.LoadScene(sceneName);
                
                if (state != null && state.talks != null)
                {
                    var save = state.talks.Find(t => t.id == talkId);
                    if (save != null && save.isFinished)
                    {
                        _isActuallyFinished = true;
                        _currentStageIndex = talkStages.Count - 1;
                        _isCurrentStagePlayed = true; 
                        
                        if (disableObjectAfterFinished) DisableSelfNowOrDelayed(true, 0f);
                        else if (disableTalkAfterFinished) DisableTalk();
                        
                        return;
                    }
                }
            }

            for (int i = 0; i < talkStages.Count; i++)
            {
                var stage = talkStages[i];
                if (stage.enableMode == TalkEnableMode.AfterEnemyDead && !string.IsNullOrEmpty(stage.targetEnemyId))
                {
                    if (TalkEnableUtil.IsEnemyDeadInScene(stage.targetEnemyId))
                    {
                        _currentStageIndex = i;
                        _isCurrentStagePlayed = false; 
                    }
                }
            }
        }

        private void TryAdvanceToNextStage()
        {
            if (talkStages == null || talkStages.Count == 0) return;

            bool isLastStage = (_currentStageIndex >= talkStages.Count - 1);

            if (!isLastStage)
            {
                var nextStage = talkStages[_currentStageIndex + 1];
                bool canAdvance = false;

                if (nextStage.enableMode == TalkEnableMode.AfterEnemyDead && !string.IsNullOrEmpty(nextStage.targetEnemyId))
                {
                    canAdvance = TalkEnableUtil.IsEnemyDeadInScene(nextStage.targetEnemyId);
                }
                else if (nextStage.enableMode == TalkEnableMode.Always && _isCurrentStagePlayed)
                {
                    canAdvance = true;
                }

                if (canAdvance)
                {
                    _currentStageIndex++;
                    _isCurrentStagePlayed = false;
                    RefreshEnableState();
                }
            }
            else
            {
                if (_isCurrentStagePlayed)
                {
                    if (!_isActuallyFinished)
                    {
                        _isActuallyFinished = true;
                        
                        if (!IsMiniGame)
                        {
                            string sceneName = SceneManager.GetActiveScene().name;
                            SceneSaveSystem.SetTalkFinished(sceneName, talkId, true);
                        }
                    }
                    
                    if (disableTalkAfterFinished) DisableTalk();
                    if (disableObjectAfterFinished) DisableSelfNowOrDelayed(true, disableDelay);
                }
            }
        }

        protected InteractContext BuildContext()
        {
            if (talkStages == null || talkStages.Count == 0) 
            {
                return new InteractContext
                {
                    owner = this, 
                    talkId = talkId, 
                    controller = controller, 
                    position = transform.position
                };
            }
            
            var currentStage = talkStages[Mathf.Clamp(_currentStageIndex, 0, talkStages.Count - 1)];
            return BuildContext(currentStage);
        }

        private InteractContext BuildContext(TalkStage stage)
        {
            bool isLastStage = (_currentStageIndex == talkStages.Count - 1);
            string safeRewardKey = IsFinished ? string.Empty : stage.rewardKey; 

            return new InteractContext
            {
                owner = this, 
                talkId = talkId, 
                controller = controller, 
                talkData = stage.talkData, 
                targetEnemyId = stage.targetEnemyId,
                rewardKey = safeRewardKey, 
                disableTalkAfterFinished = disableTalkAfterFinished && isLastStage, 
                disableObjectAfterFinished = disableObjectAfterFinished && isLastStage,
                disableDelay = disableDelay, 
                actionMode = stage.actionMode, 
                position = transform.position
            };
        }

        private void RefreshEnableState()
        {
            if (_isForceDisabled)
            {
                _isTalkEnabled = false;
                return;
            }

            if (IsMiniGame && (talkStages == null || talkStages.Count == 0))
            {
                _isTalkEnabled = true;
                return;
            }

            if (talkStages == null || talkStages.Count == 0 || _currentStageIndex >= talkStages.Count)
            {
                _isTalkEnabled = false;
                return;
            }

            var currentStage = talkStages[_currentStageIndex];

            if (_isCurrentStagePlayed && !currentStage.isRepeatable)
            {
                _isTalkEnabled = false;
                return;
            }

            if (IsFinished && !currentStage.isRepeatable)
            {
                _isTalkEnabled = false;
                return;
            }

            if (currentStage.enableMode == TalkEnableMode.Always)
            {
                _isTalkEnabled = true;
            }
            else if (currentStage.enableMode == TalkEnableMode.AfterEnemyDead)
            {
                if (string.IsNullOrEmpty(currentStage.targetEnemyId))
                    _isTalkEnabled = false;
                else
                    _isTalkEnabled = TalkEnableUtil.IsEnemyDeadInScene(currentStage.targetEnemyId);
            }
        }

        private void TryInteract()
        {
            if (!_isTalkEnabled) return;
            if (!_isDefault || _isStartTalk) return;

            if (IsMiniGame && (talkStages == null || talkStages.Count == 0))
            {
                if (Action != null)
                {
                    Action.Setup(BuildContext());
                    if (!Action.CanExecute()) return;
                    
                    if (Action is TalkRewardAction)
                    {
                        Bus<TalkFinished>.Raise(new TalkFinished(talkId, 
                            string.Empty, string.Empty, transform.position));
                        return; 
                    }

                    _isStartTalk = true;
                    Action.Execute();
                }
                return;
            }

            TryAdvanceToNextStage();

            if (_currentStageIndex >= talkStages.Count) return;

            var currentStage = talkStages[_currentStageIndex];

            if (Action == null) return;
            
            Action.Setup(BuildContext(currentStage));

            if (!Action.CanExecute()) return;

            if (currentStage.talkData == null && Action is TalkRewardAction)
            {
                if (!string.IsNullOrEmpty(currentStage.rewardKey) && !IsFinished)
                {
                    Bus<TalkFinished>.Raise(new TalkFinished(talkId, currentStage.targetEnemyId, currentStage.rewardKey, transform.position));
                }
                
                _isCurrentStagePlayed = true;
                TryAdvanceToNextStage();
                RefreshEnableState();
                return;
            }

            _isStartTalk = true;
            Action.Execute();
            _isCurrentStagePlayed = true;
        }

        public void SetStartTalkFlag(bool value)
        {
            _isStartTalk = value;
            if (!value)
            {
                TryAdvanceToNextStage();
                RefreshEnableState();
            }
        }

        public void DisableSelfNowOrDelayed(bool disableObject, float delay)
        {
            if (!disableObject) return;

            if (delay <= 0f) 
                gameObject.SetActive(false);
            else 
                Invoke(nameof(DisableSelf), delay);
        }

        private void DisableSelf() => gameObject.SetActive(false);
        
        public void DisableTalk() 
        { 
            _isTalkEnabled = false; 
            _isForceDisabled = true;
        }

        private void ChangePlayer()
        {
            _isDefault = _isOnPlayer;
            _isOnPlayer = Physics2D.CircleCast(transform.position, sensingRange, Vector2.zero, 0f, playerLayer);

            bool shouldPopup = (_isDefault != _isOnPlayer) && _isTalkEnabled;
            if (shouldPopup) _talkButtonPanel.PopUpDown();

            _isDefault = _isOnPlayer;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = rangeColor;
            Gizmos.DrawWireSphere(transform.position, sensingRange);
        }
        
    }
}