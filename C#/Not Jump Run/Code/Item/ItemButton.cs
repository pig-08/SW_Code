using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private Image _itemImage;

    private void Awake()
    {
        _itemImage = GetComponent<Image>();
    }

    public void OnButtonClick()
    {
        _itemImage.DOFade(1,0.1f);
    }
}
