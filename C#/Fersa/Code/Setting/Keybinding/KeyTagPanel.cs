using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyTagPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ketTagText;
    public void InitPanel(string tagText, List<InputAction> actionList)
    {
        ketTagText.SetText(tagText);
    }
}
