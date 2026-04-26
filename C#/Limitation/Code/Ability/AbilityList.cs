using GMS.Code.Core.Events;
using SHS.Abilities;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Cord.Ability
{
    public class AbilityList : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO AugmentationChangeEventChannelSO;
        [SerializeField] private GameObject abilityPanelPrefab;

        private Dictionary<AbilitySO, AbilityPanel> _abilitySODic = new();

        private void Awake()
        {
            AugmentationChangeEventChannelSO.AddListener<AbilityGameEvent>(SetPanel);
        }

        private void OnDestroy()
        {
            AugmentationChangeEventChannelSO.RemoveListener<AbilityGameEvent>(SetPanel);
        }

        private void SetPanel(AbilityGameEvent evt)
        {
            if(_abilitySODic.TryGetValue(evt.ability, out AbilityPanel panel))
                panel.UpgradeAbility();
            else
            {
                AbilityPanel newPanel = Instantiate(abilityPanelPrefab, transform).GetComponent<AbilityPanel>();
                newPanel.Init(evt.ability);
                _abilitySODic.Add(evt.ability, newPanel);
            }
        }
    }
}