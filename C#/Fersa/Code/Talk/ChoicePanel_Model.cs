using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Talk
{
    public class ChoicePanel_Model : MonoBehaviour
    {
        [SerializeField] private GameObject choicePrefab;
        [SerializeField] private Transform choiceButtonTrm;

        public List<ChoiceButton> choiceButtonList { get; private set; } = new List<ChoiceButton>();

        public GameObject GetPrefab() => choicePrefab;
        public Transform GetButtonPanelTrm() => choiceButtonTrm;
    }
}