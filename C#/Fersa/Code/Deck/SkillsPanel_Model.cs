using System;
using System.Collections.Generic;
using System.Linq;
using PSW.Code.EventBus;
using UnityEngine;
using YIS.Code.Events;
using YIS.Code.Skills;

namespace PSW.Code.Deck
{
    public class SkillsPanel_Model : MonoBehaviour
    {
        [field:SerializeField] public GameObject SkillIcon_CirclePrefab { private set; get;}
        [field:SerializeField] public Transform Content { private set; get; }
        [field: SerializeField] public DeckSkillDataListSO DeckDataList { private set; get; }
        [field: SerializeField] public SkillDataListSO StartDeckSkillDataList { private set; get; }

        private Dictionary<SkillDataSO, SkillIcon_Circle_Controller> _skillIconDic = new Dictionary<SkillDataSO, SkillIcon_Circle_Controller>();

        public void AddDic(SkillDataSO skillData, SkillIcon_Circle_Controller skillIcon)
        {
            if(_skillIconDic.ContainsKey(skillData) == false)
            {
                _skillIconDic.Add(skillData, skillIcon);
            }
        }
        public bool GetIsSkill(SkillDataSO skillData) => _skillIconDic.ContainsKey(skillData);
        public SkillIcon_Circle_Controller GetSkillIcon(SkillDataSO skillData) => _skillIconDic.GetValueOrDefault(skillData);
        public SkillIcon_Circle_Controller SkillIcon_Circle { set; get;}
        public List<SkillIcon_Circle_Controller> GetSkillIconList() => _skillIconDic.Values.ToList();
        public void ClearDic() => _skillIconDic.Clear();
    }
}