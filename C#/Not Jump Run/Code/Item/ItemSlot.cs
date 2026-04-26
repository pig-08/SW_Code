using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI countText;

    private ItemComponent _item;

    public void SetIcon(ItemComponent item)
    {
        _item = item;
        icon.sprite = item?.ItemIcon;

        if (item != null && item.IsDisposable)
            countText.text = item.ItemCount.ToString();
        else
            countText.text = "";
    }

    public void SetText()
    {
        if (_item != null && _item.IsDisposable)
            countText.text = _item.ItemCount.ToString();
        else
            countText.text = "";
    }

    public virtual void PickUp()
    {
        SetText();
        icon.DOFade(1f, 0.2f);
        icon.transform.DOScale(new Vector3(1.1f,1.1f,1.1f),0.2f);
        icon.DOColor(Color.yellow,0.2f);
    }

    public virtual void PickDown()
    {
        SetText();
        icon.DOFade(0.8f, 0.2f);
        icon.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        icon.DOColor(Color.white, 0.2f);
    }

}
