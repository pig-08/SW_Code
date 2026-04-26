using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Image skinImage;

    private SkinDataSO _dataSO;
    private SkinPanel _manager;
    private Button _thisButton;

    public void InitSkinButton(SkinDataSO skinDataSO, SkinPanel manager)
    {
        _dataSO = skinDataSO;
        _thisButton = GetComponent<Button>();
        _manager = manager;
        _thisButton.onClick.AddListener(SetSkin);

        skinImage.sprite = skinDataSO.skinImage;
        skinImage.color = skinDataSO.color;
        skinImage.transform.rotation = Quaternion.Euler(0, 0, skinDataSO.rotation);
        if (skinDataSO.skinMap != null)
            skinImage.material = skinDataSO.skinMap;
    }

    private void SetSkin()
    {
        _manager.SetSkin(_dataSO);
    }

    public void SetScale(float scaleValue)
    {
        transform.DOScale(new Vector2(1 + scaleValue, 1 + scaleValue), 0.2f);
    }
}