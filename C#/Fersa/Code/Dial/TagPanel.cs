using CIW.Code.System.Events;
using DG.Tweening;
using PSW.Code.EventBus;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.Dial
{
    public class TagPanel : MonoBehaviour
    {
        [SerializeField] private float doTime = 0.1f;
        [SerializeField] private TextMeshProUGUI chainTagText;
        [SerializeField] private TextMeshProUGUI skillTagText;

        private CanvasGroup _group;


        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
            _group.alpha = 0;
            _group.interactable = false;
            _group.blocksRaycasts = false;
            Bus<ShowTooltipEvent>.OnEvent += HandleShowTooltip;
            Bus<PointerExitEvent>.OnEvent += HandlePointerExit;
        }
        private void OnDestroy()
        {
            Bus<ShowTooltipEvent>.OnEvent -= HandleShowTooltip;
            Bus<PointerExitEvent>.OnEvent -= HandlePointerExit;
        }

        private void HandleShowTooltip(ShowTooltipEvent evt)
        {
            if(evt.skillData == null) return;

            BaseSkill skill = evt.skillData.skillPrefab.GetComponent<BaseSkill>();

            if (skill != null)
            {
                _group.DOFade(1f, doTime)
                    .OnComplete(() =>
                    {
                        _group.interactable = true;
                        _group.blocksRaycasts = true;
                    });

                Type derivedType = skill.GetType();

                Type[] allInterfaces = derivedType.GetInterfaces();
                Type[] parentInterfaces = derivedType.BaseType?.GetInterfaces() ?? Type.EmptyTypes;

                var directInterfaces = allInterfaces.Except(parentInterfaces)
                                     .Select(i => i.Name);

                string skillinterfaceName = "";
                string chainTaginterfaceName = "";

                foreach (string directInterName in directInterfaces)
                    skillinterfaceName += (directInterName + '\n');

                string[] interfaceNames = evt.skillData.checkAttributeName.Split(',').Select(s => s.Trim()).ToArray();

                foreach (string interfaceName in interfaceNames)
                    chainTaginterfaceName += (interfaceName + '\n');

                skillTagText.SetText(skillinterfaceName);
                chainTagText.SetText(chainTaginterfaceName.Length == 1? "체이닝이 불가능한 스킬입니다." : chainTaginterfaceName);
            }
        }

        private void HandlePointerExit(PointerExitEvent evt)
        {
            _group.DOFade(0f, doTime)
                    .OnComplete(() =>
                    {
                        _group.interactable = false;
                        _group.blocksRaycasts = false;
                    });
        }

    }
}