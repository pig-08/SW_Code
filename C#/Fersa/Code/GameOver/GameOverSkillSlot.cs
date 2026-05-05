using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.GameOver
{
    public class GameOverSkillSlot : GameOverSlotCompo
    {
        [SerializeField] private Image outLine;


        public void SetOutLineColor(Color color) => outLine.color = color;
    }
}