using YIS.Code.Skills;

namespace PSW.Code.GameOver
{
    public class SkillPanel : GameOverPanelCompo<SkillDataSO, GameOverSkillSlot>
    {
        public override GameOverSkillSlot NewSlot(SkillDataSO slotData)
        {
            GameOverSkillSlot slot = Instantiate(slotPrefab, content).GetComponent<GameOverSkillSlot>();
            slot.Init(slotData.visualData.icon);
            return slot;
        }
    }
}