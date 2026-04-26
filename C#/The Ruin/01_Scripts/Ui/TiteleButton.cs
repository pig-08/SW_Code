using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TiteleButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private string text;
    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        text = buttonText.text;
    }
    private void Update()
    {
        if (IsPointerOverUI()) buttonText.text = $"< {text} >";
        else buttonText.text = text;
          
    }

    private bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == LayerMask.NameToLayer("UI") && results[i].gameObject.name == gameObject.name)
                return true;
        }
        return false;
    }
}
