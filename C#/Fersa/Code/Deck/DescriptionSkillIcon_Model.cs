using PSW.Code.EventBus;
using UnityEngine;

namespace PSW.Code.Dial.Slot
{
    public class DescriptionSkillIcon_Model : SkillIconSetting_Model
    {
        private bool _isDonChangeData;

        public void SetDonChangeData(bool isDonChangeData) => _isDonChangeData = isDonChangeData;
        public bool GetDonChangeData() => _isDonChangeData;
    }

    public struct DonChangeSkillDataEvent : IEvent
    {
        public bool isDonChange;

        public DonChangeSkillDataEvent(bool isDonChange)
        {
            this.isDonChange = isDonChange;
        }
    }

}