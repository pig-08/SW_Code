using UnityEngine;

namespace PSW.Code.Deck
{
    public class InSkillPanel_Model : ModelCompo<object>
    {
        [field: SerializeField] public DeckSkillDataListSO DeckDataList { private set; get; }
        [field: SerializeField] public SkillsPanel_Controller SkillsPanel { private set; get; }

        private int _index;
        private bool _isPoolList;

        public SkillIcon_Circle_Controller[] SkillIcon_Circles {set; get;}
        public DeckSkillSlot_Controller[] DeckSkillSlots { set; get; }

        public override object InitModel()
        {
            return GetModelData();
        }
        public bool GetIsPoolList() => _isPoolList;
        public int GetIndex() => _index;
        public void SetIndex()
        {
            _isPoolList = false;
            for(int i = 0; i < DeckSkillSlots.Length; ++i)
            {
                if (DeckDataList.deckInSkills[i] == null)
                {
                    print(_index);
                    _index = i;
                    return;
                }
            }
            _isPoolList = true;
        }
    }
}