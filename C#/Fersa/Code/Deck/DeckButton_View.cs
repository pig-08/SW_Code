using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckButton_View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deckText;
    [SerializeField] private Image buttonImage;

    public void SetIndexText(int index) => deckText.SetText($"Deck {index}");
    public void SetImage(Sprite sprite) => buttonImage.sprite = sprite;
}
