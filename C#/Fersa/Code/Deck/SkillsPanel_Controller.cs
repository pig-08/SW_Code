using System;
using PSB.Code.BattleCode.Events;
using PSW.Code.EventBus;
using UnityEngine;
using YIS.Code.Events;
using YIS.Code.Skills;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.GlobalIllumination;
using System.Collections;

namespace PSW.Code.Deck
{
    public class SkillsPanel_Controller : MonoBehaviour
    {
        [SerializeField] private SkillsPanel_Model model;
        public Action<bool> OnButtonClickEvent;
        public event Action<SkillIcon_Circle_Controller> OnAddSkill;

        private void Awake()
        {
            Bus<SkillUpdateEvent>.OnEvent += HandleSkillUpdate;
            Bus<VillageResetEvent>.OnEvent += HandleVillageReset;
            StartSkill();
        }

        private void StartSkill()
        {
            foreach (SkillDataSO skillData in model.StartDeckSkillDataList.skills)
            {
                SkillIcon_Circle_Controller skillIcon_Circle = Instantiate(model.SkillIcon_CirclePrefab, model.Content)
                    .GetComponentInChildren<SkillIcon_Circle_Controller>();
                skillIcon_Circle.SetSkill(skillData);
                skillIcon_Circle.OnClickEvnet += ClickSkill;
                skillIcon_Circle.PopUp(model.DeckDataList.IsInSkill(skillData) == false);

                model.AddDic(skillData, skillIcon_Circle);
            }
        }

        private void HandleVillageReset(VillageResetEvent evt)
        {
            ReSetSkills();
            Debug.Log("<color=purple>마을 초기화 - 장착 스킬</color>");
        }

        private IEnumerator AllDestroySkill()
        {
            yield return null;

            foreach (SkillIcon_Circle_Controller skillIcon in model.GetSkillIconList())
                skillIcon.DestroySkill();

            model.ClearDic();

            StartSkill();
        }

        private void HandleSkillUpdate(SkillUpdateEvent evt)
        {
            if (evt.Skills == null) return;

            if (evt.Skills.Count == 0)
            {
                if (this == null) return;
                StartCoroutine(AllDestroySkill());
                return;
            }

            foreach (SkillDataSO skillData in evt.Skills)
            {
                if (model.GetIsSkill(skillData)) continue;

                SkillIcon_Circle_Controller skillIcon_Circle = Instantiate(model.SkillIcon_CirclePrefab, model.Content)
                    .GetComponentInChildren<SkillIcon_Circle_Controller>();
                skillIcon_Circle.SetSkill(skillData);
                skillIcon_Circle.OnClickEvnet += ClickSkill;
                skillIcon_Circle.PopUp(true);
                model.AddDic(skillData, skillIcon_Circle);
            }

            foreach (SkillDataSO skillData in model.DeckDataList.deckInSkills)
            {
                if (skillData == null)
                    continue;

                if(model.GetIsSkill(skillData))
                    model.GetSkillIcon(skillData).PopUp(false);
            }
        }
        
        private void OnDestroy()
        {
            Bus<VillageResetEvent>.OnEvent -= HandleVillageReset;
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
                ReSetSkills();
        }
        #endif

        public void ReSetSkills()
        {
            foreach(SkillIcon_Circle_Controller skill in model.GetSkillIconList())
                skill.PopUp(true);
        }

        private void ClickSkill(SkillIcon_Circle_Controller skillIcon_Circle)
        {
            OnAddSkill?.Invoke(skillIcon_Circle);
        }

        public void PopUpSkillIcon(SkillDataSO data)
        {
            model.GetSkillIcon(data).PopUp(true);
        }
    }
}