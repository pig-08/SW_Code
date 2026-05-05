using DG.Tweening;
using UnityEngine;

namespace PSW.Code.Deck
{
    public class DeckButtonPanel_Controller : MonoBehaviour
    {
        [SerializeField] private DeckButtonPanel_Model model;

        private void Awake()
        {
            for (int i = 0; i < model.DeckDataList.deckDataList.Count; ++i)
                AddButton(i == model.DeckDataList.GetIndex());

            model.GetParentTrm()
                .DOLocalMoveY(model.GetMoveCurrentValue
                (model.DeckDataList.GetIndex()), 0);
        }

        public async void AddButton(bool isOnButton = true)
        {
            GameObject button = Instantiate(model.GetButtonPrefab(), model.GetParentTrm());
            DeckButton_Controller deckButton = button.GetComponent<DeckButton_Controller>();
            deckButton.InitButton(model.GetParentTrm().childCount-1);
            deckButton.OnButtonClickEvent += ClickNewButton;
            await Awaitable.NextFrameAsync();
            
            if(isOnButton)
                deckButton.ClickButton();
        }

        public void ClickNewButton(DeckButton_Controller deckButton)
        {
            model.GetDeckButton()?.SetImage(false);
            model.SetButton(deckButton);
        }
    }
}