using CIW.Code.System.Events;
using DG.Tweening;
using PSB.Code.BattleCode.Events;
using PSW.Code.Dial;
using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Work.CSH.Scripts.Interfaces;
using Work.CSH.Scripts.Managers;
using YIS.Code.Skills;

namespace PSW.Code.CombinationSkill
{
    public class CombinationSkillPanel : MonoBehaviour, ITurnable
    {
        [field: SerializeField] public TurnManagerSO TurnManager { get; set; }
        [SerializeField] private Animator allChainingAnimator;
        [SerializeField] private AnimationClip allChainingClip;
        [SerializeField] private float allChainingDelayTime = 0.5f;
        [SerializeField] private Material jinMap;
        [SerializeField] private Material outLineMap;
         
        [SerializeField] private CheckValueData[] nextValueList = new CheckValueData[3];

        [SerializeField] private string setFloatValue = "Bloom Power";
        [SerializeField] private float popUpValue = 3;
        [SerializeField] private float popDownValue = 0;

        private List<ChoiceSkillSlot> _choiceSkillSlotList = new List<ChoiceSkillSlot>();
        private SelectSkillsEvent _currentEndSkillEvent = new SelectSkillsEvent();

        private SkillDataSO[] _skillDatas = new SkillDataSO[3];
        private bool[] _setSkillIndex = new bool[3];

        private bool _buttonClick = false;
        private bool _isOneInSkill = false;

        private int _previewSlotIndex = -1;
        private readonly List<int> _previewHistory = new();

        private ChoiceSkillSlot _inSkillCurrentSlot;
        private ChoiceSkillSlot _notSkillCurrentSlot;
        

        private void Awake()
        {
            jinMap.SetFloat(setFloatValue, 0);
            outLineMap.SetFloat(setFloatValue, 0);

            _choiceSkillSlotList = GetComponentsInChildren<ChoiceSkillSlot>().ToList();
            _choiceSkillSlotList.ForEach(slot =>
            {
                slot.OnNewSkill += AddSkill;
                slot.OnCurrentSlotEvent += SetCurrentSlot;
                slot.OnNotSkillClickEvnet += ChangeSkillSlot;
            });

            TurnManager.AddITurnableList(this);

            Bus<ChainingEvent>.OnEvent += Chaining;
            Bus<UnChainingEvent>.OnEvent += UnChaining;
            Bus<AllChainingEvent>.OnEvent += AllChaining;
            Bus<GetIsCurrentSlot>.OnEvent += ChangeSkill;

            Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(0));
            Bus<SkillDamagePreviewEvent>.Raise(new SkillDamagePreviewEvent(null, null, -1));
        }

        private void OnDestroy()
        {
            _choiceSkillSlotList.ForEach(slot =>
            {
                slot.OnNewSkill -= AddSkill;
                slot.OnCurrentSlotEvent -= SetCurrentSlot;
                slot.OnNotSkillClickEvnet -= ChangeSkillSlot;
            });
            TurnManager.RemoveITurnableList(this);

            Bus<ChainingEvent>.OnEvent -= Chaining;
            Bus<UnChainingEvent>.OnEvent -= UnChaining;
            Bus<AllChainingEvent>.OnEvent -= AllChaining;
            Bus<GetIsCurrentSlot>.OnEvent -= ChangeSkill;
        }

        private async void AllChaining(AllChainingEvent evt)
        {
            allChainingAnimator.Play(allChainingClip.name, 0, 0);
            await Awaitable.WaitForSecondsAsync(allChainingClip.length + allChainingDelayTime);
            allChainingAnimator.Play("Remove" + allChainingClip.name, 0, 0);
            evt.callback?.Invoke();
        }

        private void UnChaining(UnChainingEvent evt) => evt.callback?.Invoke();

        private void Chaining(ChainingEvent evt)
        {
            _choiceSkillSlotList[evt.myIndex].SkillSlot_Visual.ChainEffect(evt.myIndex > evt.targetIndex, evt.callback);
            _choiceSkillSlotList[evt.targetIndex].StartChain();
        }

        public void AddSkill(SkillDataSO skillData, int index, bool isInIcon,bool isUnequipped = true)
        {
            jinMap.DOFloat(popDownValue, setFloatValue, 0.3f);
            outLineMap.DOFloat(popDownValue, setFloatValue, 0.3f);

            _notSkillCurrentSlot?.SkillSlot_Visual.SetSelectBox(false);
            _notSkillCurrentSlot = null;
            Bus<ChangeCurrentSlot>.Raise(new ChangeCurrentSlot(null));

            SkillDataSO prev = _skillDatas[index];
            
            if (prev != null && (skillData == null || prev != skillData) && isUnequipped)
                Bus< SkillIconUnequippedEvent>.Raise(new SkillIconUnequippedEvent(prev));

            _skillDatas[index] = skillData;
            _setSkillIndex[index] = isInIcon;

            if (skillData != null && isInIcon)
                Bus<SkillIconEquippedEvent>.Raise(new SkillIconEquippedEvent(skillData));

            InSkillChain();
            IsOneSkillIn();
            UpdateRangePreviewOnSlotChanged(index);
        }
        
        private void UpdateRangePreviewOnSlotChanged(int changedIndex)
        {
            if (changedIndex >= 0 && _skillDatas[changedIndex] != null && _setSkillIndex[changedIndex])
            {
                _previewSlotIndex = changedIndex;
                _previewHistory.Remove(changedIndex);
                _previewHistory.Add(changedIndex);
            }
            else
            {
                _previewHistory.Remove(changedIndex);
                _previewSlotIndex = -1;
                for (int i = _previewHistory.Count - 1; i >= 0; i--)
                {
                    int idx = _previewHistory[i];
                    if (idx >= 0 && _skillDatas[idx] != null && _setSkillIndex[idx])
                    {
                        _previewSlotIndex = idx;
                        break;
                    }
                    _previewHistory.RemoveAt(i);
                }
            }

            if (_previewSlotIndex == -1)
            {
                ResetPreview();
                return;
            }

            Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(_skillDatas[_previewSlotIndex].range));
            Bus<SkillDamagePreviewEvent>.Raise(new SkillDamagePreviewEvent(_skillDatas, _setSkillIndex, _previewSlotIndex));
        }

        private void ResetPreview()
        {
            _previewSlotIndex = -1;
            Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(0));
            Bus<SkillDamagePreviewEvent>.Raise(new SkillDamagePreviewEvent(null, null, -1));
        }

        private void InSkillChain()
        {
            List<(SkillDataSO, bool)> tempList = new List<(SkillDataSO, bool)>();
            for(int i = 0; i < _choiceSkillSlotList.Count; ++i)
            {
                tempList.Clear();
                CheckValueData tempData = nextValueList[i];

                foreach (ValueData data in tempData.valueDataList)
                {
                    if (data.index >= 0 && data.index < _skillDatas.Length)
                    {
                        print($"테스트 {i}: {_skillDatas[data.index] == null} , {data.index}");
                        tempList.Add((_skillDatas[data.index], data.isLeft));
                    }
                }

                if (_choiceSkillSlotList[i].SlotSkillIcon != null)
                    _choiceSkillSlotList[i].SlotSkillIcon.NextChain(tempList, true ,false);
            }
        }

        public async void EndChaining()
        {
            await Awaitable.WaitForSecondsAsync(0.5f);
            ChangeSkillSlot(null);

            Bus<SkillIconClearAllEvent>.Raise(new SkillIconClearAllEvent());

            for (int i = 0; i < _setSkillIndex.Length; ++i)
                _setSkillIndex[i] = false;

            for (int i = 0; i < _skillDatas.Length; ++i)
                _skillDatas[i] = null;

            _choiceSkillSlotList.ForEach(slot =>
            {
                if (slot.SlotSkillIcon != null)
                {
                    slot.SlotSkillIcon.SetSkill(null);
                    slot.SlotSkillIcon.NextChainNot();
                }
                slot.SetPullSlot(false);
                slot.SetGraphicRaycaster(true);
            });

            _buttonClick = false;
            _isOneInSkill = false;

            _previewSlotIndex = -1;
            _previewHistory.Clear();
            Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(0));
            Bus<SkillDamagePreviewEvent>.Raise(new SkillDamagePreviewEvent(null, null, -1));
        }

        public void IsOneSkillIn()
        {
            foreach (bool inSkill in _setSkillIndex)
            {
                if(inSkill)
                {
                    _isOneInSkill = true;
                    return;
                }
            }
            _isOneInSkill = false;
        }

        public void AttackStart()
        {
            if (_buttonClick || _isOneInSkill == false)
                return;

            SetCurrentSlot(null);
            _buttonClick = true;
            _choiceSkillSlotList.ForEach(slot => slot.SetPullSlot(true));
            _currentEndSkillEvent.skillDatas = _skillDatas;

            Bus<SelectSkillsEvent>.Raise(_currentEndSkillEvent);
        }

        public void OnStartTurn(bool isPlayerTurn)
        {
            if (!isPlayerTurn) return;

            _choiceSkillSlotList.ForEach(v => v.PopUp(false));

            for (int i = 0; i < _skillDatas.Length; ++i)
                _skillDatas[i] = null;

            if (_previewSlotIndex >= 0 &&
                _previewSlotIndex < _skillDatas.Length &&
                _skillDatas[_previewSlotIndex] != null &&
                _setSkillIndex[_previewSlotIndex])
            {
                Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(_skillDatas[_previewSlotIndex].range));
                return;
            }
            
            for (int i = _previewHistory.Count - 1; i >= 0; i--)
            {
                int idx = _previewHistory[i];
                if (idx < 0 || idx >= _skillDatas.Length)
                {
                    _previewHistory.RemoveAt(i);
                    continue;
                }

                if (_skillDatas[idx] != null && _setSkillIndex[idx])
                {
                    _previewSlotIndex = idx;
                    Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(_skillDatas[idx].range));
                    return;
                }

                _previewHistory.RemoveAt(i);
            }

            _previewSlotIndex = -1;
            Bus<SkillRangePreviewEvent>.Raise(new SkillRangePreviewEvent(0));
        }

        public void OnEndTurn(bool isPlayerTurn)
        {
            if (isPlayerTurn)
                EndChaining();
        }

        private void SetMap(bool isPopUp)
        {
            if (isPopUp)
            {
                jinMap.DOFloat(popUpValue, setFloatValue, 0.3f);
                outLineMap.DOFloat(5, setFloatValue, 0.3f);
            }
            else
            {
                jinMap.DOFloat(popDownValue, setFloatValue, 0.3f);
                outLineMap.DOFloat(popDownValue, setFloatValue, 0.3f);
            }
        }

        private void ChangeSkill(GetIsCurrentSlot evt)
        {
            if (_notSkillCurrentSlot != null)
            {
                _notSkillCurrentSlot.InvokeGetSkillDataEvent();
                _notSkillCurrentSlot?.SkillSlot_Visual.SetSelectBox(false);
                _notSkillCurrentSlot = null;
            }
            if (_inSkillCurrentSlot != null)
                _inSkillCurrentSlot.InvokeGetSkillDataEvent();
        } 

        public void ChangeSkillSlot(ChoiceSkillSlot skillSlot)
        {
            if (_buttonClick)
                return;

            if (_inSkillCurrentSlot != null && _inSkillCurrentSlot.SlotSkillIcon != null)
            {
                skillSlot.SetSkill(_inSkillCurrentSlot.SlotSkillIcon.GetSkill(),
                    _inSkillCurrentSlot.SlotSkillIcon.GetPopTime());
                _inSkillCurrentSlot.SkillItOff(false);
                _inSkillCurrentSlot = null;
            }
            else
            {
                _notSkillCurrentSlot?.SkillSlot_Visual.SetSelectBox(false);

                if (_notSkillCurrentSlot != skillSlot)
                {
                    _notSkillCurrentSlot = skillSlot;

                    int slotIdx = _choiceSkillSlotList.IndexOf(_notSkillCurrentSlot);
                    if (slotIdx >= 0)
                    {
                        CheckValueData tempData = nextValueList[slotIdx];
                        List<(SkillDataSO,bool)> tempList = new List<(SkillDataSO, bool)>();

                        foreach (ValueData data in tempData.valueDataList)
                        {
                            if (data.index >= 0 && data.index < _skillDatas.Length)
                                tempList.Add((_skillDatas[data.index], data.isLeft));
                        }

                        Bus<ChangeCurrentSlot>.Raise(new ChangeCurrentSlot(tempList));
                    }
                }
                else
                {
                    _notSkillCurrentSlot = null;
                    Bus<ChangeCurrentSlot>.Raise(new ChangeCurrentSlot(null));
                }

                _notSkillCurrentSlot?.SkillSlot_Visual.SetSelectBox(true);
            }
        }

        public void SetCurrentSlot(ChoiceSkillSlot slotSkillIcon)
        {
            if (_buttonClick)
                return;

            if (_inSkillCurrentSlot != null && _inSkillCurrentSlot.SlotSkillIcon != null)
                _inSkillCurrentSlot.SlotSkillIcon.SetCurrentButton(false);

            if (_inSkillCurrentSlot != slotSkillIcon && slotSkillIcon != null)
            {
                if(_inSkillCurrentSlot != null)
                {
                    SlotSkillIcon_Controller current = _inSkillCurrentSlot.SlotSkillIcon;
                    SlotSkillIcon_Controller add = slotSkillIcon.SlotSkillIcon;

                    if (current != null && add != null)
                    {
                        current.PopUp(false);
                        add.PopUp(false);

                        // 슬롯 변경 전 데이터를 미리 캡처
                        SkillDataSO currentSkillData = current.GetSkill();
                        SkillDataSO addSkillData = add.GetSkill();
                        float currentPop = current.GetPopTime();
                        float addPop = add.GetPopTime();

                        int currentSlotIdx = _choiceSkillSlotList.IndexOf(_inSkillCurrentSlot);
                        int targetSlotIdx = _choiceSkillSlotList.IndexOf(slotSkillIcon);

                        // UI 슬롯 스왑 진행
                        slotSkillIcon.SetSkill(currentSkillData, currentPop, false);
                        _inSkillCurrentSlot.SetSkill(addSkillData, addPop, false);

                        // 🔥 [고스트 데이터 킬러] 시뮬레이션용 배열(_skillDatas)을 현재 UI 상태와 100% 동일하게 강제 동기화합니다.
                        if (targetSlotIdx >= 0)
                        {
                            _skillDatas[targetSlotIdx] = currentSkillData;
                            _setSkillIndex[targetSlotIdx] = currentSkillData != null;
                        }
                        
                        if (currentSlotIdx >= 0)
                        {
                            _skillDatas[currentSlotIdx] = addSkillData;
                            _setSkillIndex[currentSlotIdx] = addSkillData != null;
                        }

                        SetMap(false);
                        _inSkillCurrentSlot = null;
                        Bus<ChangeCurrentSlot>.Raise(new ChangeCurrentSlot(null));

                        // 배열이 완벽하게 정리된 후 프리뷰 이벤트 갱신 호출
                        if (currentSlotIdx >= 0) UpdateRangePreviewOnSlotChanged(currentSlotIdx);
                        if (targetSlotIdx >= 0) UpdateRangePreviewOnSlotChanged(targetSlotIdx);
                    }
                }
                else if(_notSkillCurrentSlot != null)
                {
                    SlotSkillIcon_Controller controller = slotSkillIcon.SlotSkillIcon;
                    if (controller != null)
                    {
                        controller.PopUp(false);
                        SkillDataSO data = controller.GetSkill();
                        float time = controller.GetPopTime();
                        ChoiceSkillSlot tempSlot = _notSkillCurrentSlot;
                        slotSkillIcon.SkillItOff(false);
                        tempSlot.SetSkill(data, time, false);
                        _notSkillCurrentSlot = null;
                        SetMap(false);
                    }
                }
                else
                {
                    _inSkillCurrentSlot = slotSkillIcon;
                }
            }
            else
            {
                _inSkillCurrentSlot = null;
                Bus<ChangeCurrentSlot>.Raise(new ChangeCurrentSlot(null));
                SetMap(false);
            }

            if (_inSkillCurrentSlot != null && _inSkillCurrentSlot.SlotSkillIcon != null)
                _inSkillCurrentSlot.SlotSkillIcon.SetCurrentButton(true);
        }
    }

    public struct SelectSkillsEvent : IEvent
    {
        public SkillDataSO[] skillDatas;
    }

    public struct SkillIconClearAllEvent : IEvent { }

    public struct SkillIconEquippedEvent : IEvent
    {
        public SkillDataSO Skill;
        public SkillIconEquippedEvent(SkillDataSO skill) => Skill = skill;
    }

    public struct SkillIconUnequippedEvent : IEvent
    {
        public SkillDataSO Skill;
        public SkillIconUnequippedEvent(SkillDataSO skill) => Skill = skill;
    }

    [Serializable]
    public struct CheckValueData
    {
        public List<ValueData> valueDataList;
    }

    [Serializable]
    public struct ValueData
    {
        public int index;
        public bool isLeft;
    }
    
}