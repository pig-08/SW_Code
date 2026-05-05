using PSB.Code.BattleCode.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.GetItemPanel
{
    public class ItemPanel_View : MonoBehaviour, IView<DroppedItem>
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;

        public void Init(DroppedItem defaultData)
        {
            SetPanel(defaultData);
        }

        public void SetData(DroppedItem data)
        {
            SetPanel(data);
        }

        private void SetPanel(DroppedItem data)
        {
            icon.sprite = data.item.visualData.icon;
            nameText.text = data.item.itemName;
            amountText.text = $"+{data.amount.ToString()}";
        }
    }
}