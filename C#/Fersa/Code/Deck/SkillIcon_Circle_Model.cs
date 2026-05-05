using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.Deck
{
    public class SkillIcon_Circle_Model : SkillIconSetting_Model
    {
        [SerializeField] private SizeData mouseData;
        [SerializeField] private SizeData clickData;

        [SerializeField] private List<string> mouseUpEffectAnimClipNameList;

        [SerializeField] private float clickSizeChangeTime = 0.1f;
        [SerializeField] private float mouseOnTime;
        [SerializeField] private float choiceFade = 0.2f;

        private DescriptionSkillIEvent _skillIEvent = new DescriptionSkillIEvent();
        private DescriptionSkillIEvent _skillINullEvent = new DescriptionSkillIEvent();

        private bool _isOnMouse;

        public void SetSkillIData(SkillDataSO data) => _skillIEvent.skillData = data;
        public void SetUpOnMouse() => _isOnMouse = !_isOnMouse;
        public void SetUpOnMouse(bool isOnMouse) => _isOnMouse = isOnMouse;

        public string GetMouseUpEffectName(bool isUp) => isUp ? mouseUpEffectAnimClipNameList[0] : mouseUpEffectAnimClipNameList[1];
        public float GetChoiceFade() => choiceFade;
        public bool GetOnMouse() => _isOnMouse;
        public float GetMouseOnTime() => mouseOnTime;
        public float GetClickSizeChangeTime() => clickSizeChangeTime;
        public Vector3 GetMouseData() => mouseData.GetSize(_isOnMouse);
        public Vector3 GetClickData(bool isClickSize) => clickData.GetSize(isClickSize);
        public DescriptionSkillIEvent GetSkillIEvent(bool isChange = false)
        {
            _skillIEvent.changeData = isChange;
            return _skillIEvent;
        }
        public DescriptionSkillIEvent GetSkillINullEvent(bool isChange = false)
        {
            _skillINullEvent.changeData = isChange;
            return _skillINullEvent;
        }
    }

    public struct DescriptionSkillIEvent : IEvent
    {
        public bool changeData;
        public SkillDataSO skillData;
    }
}