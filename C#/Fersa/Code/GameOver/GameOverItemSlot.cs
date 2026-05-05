using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.GameOver
{
    public class GameOverItemSlot : GameOverSlotCompo
    {
        [SerializeField] private TextMeshProUGUI numberText;
        public void SetNumber(string number) => numberText.SetText(number);
    }
}