using DG.Tweening;
using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

namespace PSW.Code.Talk
{
    public class Talk_View : MonoBehaviour, IView<TalkData>
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI taklValueText;

        [Header("Character")]
        [SerializeField] private TalkCharacter leftCharacter;
        [SerializeField] private TalkCharacter rightCharacter;

        [Header("Mask")]
        [SerializeField] private RectTransform paenlMaskRectTrm;

        public void Init(TalkData defaultData)
        {
            nameText.SetText(defaultData.Name);
            taklValueText.SetText(defaultData.TaklValue);
            
            PopMask(Vector3.zero, 0);

            leftCharacter.Init(DirType.Left);
            rightCharacter.Init(DirType.Right);

        }

        public void SetData(TalkData data)
        {
            nameText.SetText(data.Name);
            taklValueText.SetText(data.TaklValue);
            SetUpCharacter(data.IsUpLeft);
        }

        public void SetCharacter(CharacterData data, DirType type)
        {
            TalkCharacter character = 
                type == DirType.Left ?
                leftCharacter : rightCharacter;

            character.PopUpCharacter(data, type);
        }

        public void PopDownTalk()
        {
            leftCharacter.PopDownCharacter();
            rightCharacter.PopDownCharacter();
        }

        public async void PopMask(Vector3 size,float time, float waitTime = 0)
        {
            if(waitTime != 0) await Awaitable.WaitForSecondsAsync(waitTime);
            paenlMaskRectTrm.DOSizeDelta(size, time);
        }

        public void SetUpCharacter(bool isLeft)
        {
            GetCharacter(isLeft).TalkSet(true);
            GetCharacter(!isLeft).TalkSet(false);
        }

        private TalkCharacter GetCharacter(bool isLeft)
        {
            if (isLeft)
                return leftCharacter;
            else
                return rightCharacter;
        }

        public void SetTaklValue(string taklData) => taklValueText.SetText(taklData);

    }
}

public struct TalkData
{
    public string Name;
    public string TaklValue;
    public bool IsUpLeft;
    public DirType type;
}