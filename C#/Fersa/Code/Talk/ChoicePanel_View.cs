using UnityEngine;

namespace PSW.Code.Talk
{
    public class ChoicePanel_View : MonoBehaviour
    {
        [SerializeField] private GameObject choicePanel;
        public void OnOffPanel(bool isOn) => choicePanel.SetActive(isOn);
    }
}