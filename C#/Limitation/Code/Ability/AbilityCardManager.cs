using GMS.Code.Core.Events;
using SHS.Abilities;
using SHS.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace PSW.Cord.Ability
{
    public class AbilityCardManager : MonoBehaviour
    {
        [SerializeField] private AbilityBahavior abilityBahavior;
        [SerializeField] private GameObject augmentationCardPrefab;
        [SerializeField] private List<AbilitySO> _abilityList;
        [SerializeField] private GameEventChannelSO AugmentationChangeEventChannelSO;

        public AwaitableCompletionSource selectCard;
        // public UnityEvent OnChoiceCardEvent;

        private List<AbilityCard> _augmentationCardList = new List<AbilityCard>();

        private void Awake()
        {
            _augmentationCardList = GetComponentsInChildren<AbilityCard>().ToList();
            selectCard = new AwaitableCompletionSource();
        }

        public async Task CreateCard()
        {
            CardShuffle();

            int shownCards = 0;
            for (int i = 0; i < _abilityList.Count; ++i)
            {
                if (shownCards >= 3) break;

                AbilitySO ability = _abilityList[i];
                if (abilityBahavior.CanAddAbility(ability) == false)
                    continue;
                await Awaitable.WaitForSecondsAsync(0.2f);
                _augmentationCardList[shownCards].InitCard(ability);
                shownCards++;
            }

            Debug.Log("���ʹ� ���� ���Ÿ����");
            selectCard.Reset();
            await selectCard.Awaitable;
        }

        public void ChoiceCard(AbilityCard choiceCard)
        {
            foreach (AbilityCard card in _augmentationCardList)
            {
                if (card == choiceCard)
                {
                    AbilitySO abilitySO = card.ChoiceCard();
                    AbilityGameEvent abilityGameEvent = new AbilityGameEvent();
                    abilityGameEvent.ability = abilitySO;
                    AugmentationChangeEventChannelSO.RaiseEvent(abilityGameEvent);
                    abilityBahavior.AddAbility(abilitySO);
                }
                else
                    card.OutCard();
            }

            // OnChoiceCardEvent?.Invoke();
            selectCard.SetResult();
        }

        private void CardShuffle()
        {
            for (int i = 0; i < 100; ++i)
            {
                int value1 = Random.Range(0, _abilityList.Count);
                int value2 = Random.Range(0, _abilityList.Count);

                AbilitySO tmep = _abilityList[value1];
                _abilityList[value1] = _abilityList[value2];
                _abilityList[value2] = tmep;
            }
        }
    }
}