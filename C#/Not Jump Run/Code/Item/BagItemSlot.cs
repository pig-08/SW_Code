using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BagItemSlot : ItemSlot
{
    [SerializeField] private Sprite notChangeImage;
    [SerializeField] private Sprite onChangeImage;

    private bool _isInit;
    private bool _isChangeOn;

    private Image _iconImage;
    public void Init()
    {
        icon.DOFade(0.8f, 0f);
        icon.transform.DOScale(new Vector3(1f, 1f, 1f), 0f);
        _isInit = true;
        _iconImage = GetComponent<Image>();
    }

    public override void PickUp()
    {
        if(_isInit && _isChangeOn == false)
            base.PickUp();
    }

    public override void PickDown()
    {
        if (_isInit && _isChangeOn == false)
            base.PickDown();
    }

    public void ChangeOn()
    {
        if (_isChangeOn) return;

        _isChangeOn = true;
        _iconImage.sprite = onChangeImage;
        base.PickUp();
    }

    public void ChangeOff()
    {
        if (_isChangeOn == false) return;

        _isChangeOn = false;
        _iconImage.sprite = notChangeImage;
        base.PickDown();
    }
}
