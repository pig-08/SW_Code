using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiedPanel : MonoBehaviour, IUi
{
    private List<Image> diedImages = new List<Image>();

    public void Init()
    {
        GetComponentsInChildren(diedImages);

        foreach (var image in diedImages)
        {
            image.DOFade(1, 0.8f);
        }
    }



}
