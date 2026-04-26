using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private SkinDataSO defaultSkinDataSO;
    [SerializeField] private SkinManager manager;
    [SerializeField] private List<SkinDataSO> skinDataSOList;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Image choiceSkinImage;


    private void Awake()
    {
        SetSkin(defaultSkinDataSO);

        foreach (SkinDataSO data in skinDataSOList)
        {
            SkinButton newButton = Instantiate(buttonPrefab, content).GetComponent<SkinButton>();
            newButton.InitSkinButton(data, this);
        }
    }

    public void SetSkin(SkinDataSO skinData)
    {
        choiceSkinImage.sprite = skinData.skinImage;
        choiceSkinImage.color = skinData.color;
        choiceSkinImage.transform.rotation = Quaternion.Euler(0, 0, skinData.rotation);
        if (skinData.skinMap != null)
            choiceSkinImage.material = skinData.skinMap;

        manager.SetChoiceSkinDataSO(skinData);
    }

    public void SetScale(float scaleValue)
    {
        content.DOMoveY(0, 0);
        transform.DOScale(new Vector2(1 + scaleValue, 1 + scaleValue), 0.3f);
    }
}