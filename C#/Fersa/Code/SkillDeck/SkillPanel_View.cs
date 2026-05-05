using PSW.Code.Dial;
using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.SkillDeck
{
    public class SkillPanel_View : MonoBehaviour, IView<List<SkillDataSO>>
    {
        [SerializeField] private Animator panelAnimator;
        private List<Slot> skillSlotList;

        public void Init(List<SkillDataSO> defaultData)
        {
            skillSlotList = GetComponentsInChildren<Slot>().ToList();
        }


        public void SetData(List<SkillDataSO> data) { }
        public void SetList(List<SkillDataSO> datas, Action<SlotIcon_Controller> clickAction)
        {
            for (int i = 0; i < datas.Count; ++i)
            {
                skillSlotList[i].SetSkillData(datas[i], clickAction);
            }
        }
        public void DowMoveSlotList()
        {
            foreach (Slot slot in skillSlotList)
                slot.DonMoveSlot();
        }

        public void PlayAnim(string name) => panelAnimator.Play(name);
        public void SetBackChild() => transform.SetSiblingIndex(transform.parent.childCount - 1);
        public void NextTurn(Action<SlotIcon_Controller> clickAction)
        {
            foreach (Slot slot in skillSlotList)
                slot.ChildSettingCompoCheck();
        }
    }
}