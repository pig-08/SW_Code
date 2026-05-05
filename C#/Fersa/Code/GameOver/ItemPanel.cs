using YIS.Code.Items;

namespace PSW.Code.GameOver
{
    public class ItemPanel : GameOverPanelCompo<ItemDataSO, GameOverItemSlot>
    {
        public override GameOverItemSlot NewSlot(ItemDataSO slotData)
        {
            GameOverItemSlot slot = Instantiate(slotPrefab, content).GetComponent<GameOverItemSlot>();
            slot.Init(slotData.visualData.icon);
            return slot;
        }
    }
}