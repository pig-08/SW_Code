using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Book
{
    public class DescriptionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform panelTrm;
        public void PopUp(Vector2 pos, string description)
        {
            panelTrm.anchoredPosition = pos;
            text.SetText(description);
            gameObject.SetActive(true);
        }
        public void PopDown()
        {
            gameObject.SetActive(false);
        }
    }
}