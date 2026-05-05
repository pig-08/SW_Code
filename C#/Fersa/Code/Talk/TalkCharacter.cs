using DG.Tweening;
using Unity.AppUI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Talk
{
    public class TalkCharacter : MonoBehaviour
    {
        [SerializeField] private RectTransform maskTrm;

        [SerializeField] private DirType currentDirType;
        [SerializeField] private Vector2 talKValue;
        
        [SerializeField] private float moveTime;
        [SerializeField] private float popTime;
        [SerializeField] private float notTalkFade = 0.3f;

        private Image _characterImage;

        private string _name;

        private Vector3 _talkPointValue;
        private Vector3 _startPointValue;
        private Vector3 _openMaskValue;

        public void Init(DirType dir)
        {
            _startPointValue = maskTrm.localPosition;
            _talkPointValue = maskTrm.localPosition;
            _openMaskValue = maskTrm.sizeDelta;
            SetCharacterDir(dir);
            SetMaskSize(false, true);
        }

        public void PopUpCharacter(CharacterData data, DirType dir)
        {
            if (data.GazeDirType == dir)
                transform.eulerAngles = new Vector3(0, 180, 0);

            _name = data.Name;
            _characterImage = GetComponent<Image>();
            _characterImage.sprite = data.CharacterImgae;
            TalkSet(false);
            SetMaskSize(true);
        }

        public async void PopDownCharacter()
        {
            TalkSet(false);
            await Awaitable.WaitForSecondsAsync(moveTime);
            SetMaskSize(false);
        }

        public void SetMaskSize(bool isOpen, bool isNotTime = false)
        {
            maskTrm.DOSizeDelta(isOpen ? _openMaskValue : Vector3.zero,
                isNotTime ? 0 : popTime);
        }

        private void SetCharacterDir(DirType dir)
        {
            float xValue = talKValue.x;
            if (dir == DirType.Right) xValue *= -1;
            _talkPointValue.x += xValue;
            _talkPointValue.y += talKValue.y;
        }

        public void TalkSet(bool isTalk)
        {
            Vector3 targetValue = isTalk ? _talkPointValue : _startPointValue;
            float targetFade = isTalk ? 1 : notTalkFade;
            maskTrm.DOLocalMove(targetValue, moveTime);
            _characterImage.DOFade(targetFade, moveTime);
        }
    }
}