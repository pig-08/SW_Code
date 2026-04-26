using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUiPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Image panel;
    [SerializeField] private Transform arrow;

    public bool IsOpanelOnOff { get; private set; }

    public void UpPanel()
    {
        IsOpanelOnOff = true;
        panel.transform.DOMoveY(0, 0.2f).SetEase(Ease.InOutBack);
    }

    public void DownPanel()
    {
        IsOpanelOnOff = false;
        panel.transform.DOMoveY(-250, 0.2f).SetEase(Ease.InOutBack);
    }

    public void ClearText() => tutorialText.text = "";
    public void SetText(char text) => tutorialText.text += text;
    public void SetUpArrow(Vector2 point, Quaternion rotation)
    {
        arrow.rotation = rotation;
        arrow.localPosition = point;
    }

    public void SetArrowOnOff(bool isOnOff) => arrow.gameObject.SetActive(isOnOff);

}
