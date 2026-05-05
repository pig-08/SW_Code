using PSB.Code.BattleCode.UIs.BossShopUI;
using PSB.Code.CoreSystem.Events;
using PSW.Code.EventBus;
using System.Collections.Generic;
using UnityEngine;
using YIS.Code.Defines;
using YIS.Code.Items;
using YIS.Code.Skills;

namespace PSW.Code.Book
{
    public class BookPanel_Controller : MonoBehaviour
    {
        [SerializeField] private BookPanel_Model model;
        private void Start()
        {
            Bus<BossShopUnlockEvent>.OnEvent += UnLockSlot;
            model.InitModel();
            InstantiateSlot();
            InstantiateToggle();
            foreach (UnlockDataSO iockData in model.lockDataListSO.shopItemDataList)
            {
                if (model.IsUnLock(iockData.id)) continue;

                switch (iockData.shopItemData.shopItemType)
                {
                    case ShopItemType.Item:
                        model.GetItemSlot(iockData.shopItemData.itemData)?.SetLock(true);
                        continue;
                    case ShopItemType.Skill:
                        model.GetSkillSlot(iockData.shopItemData.skillData)?.SetLock(true);
                        continue;
                    default:
                        continue;
                }
            }
            model.GetDescriptionPanel().PopDown();
        }

        private void OnDestroy()
        {
            Bus<BossShopUnlockEvent>.OnEvent -= UnLockSlot;
        }

        public void UnLockSlot(BossShopUnlockEvent evt)
        {
            BookSlotCompo tempSlot;
            switch (evt.Item.shopItemData.shopItemType)
            {
                case ShopItemType.Skill:
                    tempSlot = model.GetSkillSlot(evt.Item.shopItemData.skillData);
                    break;
                case ShopItemType.Item:
                    tempSlot = model.GetItemSlot(evt.Item.shopItemData.itemData);
                    break;
                default:
                    tempSlot = null;
                    break;
            }
            tempSlot?.SetLock(false);
        }

        private void InstantiateSlot()
        {
            foreach (SkillDataSO skillData in model.playerSkillList.skills)
            {
                BookSkillSlot skillSlot = model.NewSlotAndGetComponent<BookSkillSlot>(BookSlotType.Skill);
                skillSlot.Init(skillData);
                SetSlot(skillSlot);
                model.AddSkillDic(skillData, skillSlot);
            }
            SlotSort();
            foreach (ItemDataSO itemDataSO in model.itemDataListSO.Items)
            {
                BookItemSlot itemSlot = model.NewSlotAndGetComponent<BookItemSlot>(BookSlotType.Item);
                itemSlot.Init(itemDataSO);
                SetSlot(itemSlot);
                model.AddItemDic(itemDataSO, itemSlot);
            }
        }
        
        private void InstantiateToggle()
        {
            for (int i = 0; i <= 1; ++i)
            {
                TagToggleCompo<BookSlotType> skillSlot = model.NewToggleAndGetComponent((BookSlotType)i);
                skillSlot.Init((BookSlotType)i, SetSlotType);

                TagToggleCompo<BookLockType> lockSlot = model.NewToggleAndGetComponent((BookLockType)i);
                lockSlot.Init((BookLockType)i, SetLockType);
            }
        }

        private void SlotSort()
        {
            List<BookSkillSlot> slotCompoList = model.GetSkillDicList();

            for (int i = 0; i < slotCompoList.Count; ++i)
                slotCompoList[i].transform.SetSiblingIndex(i);
        }

        public void SetSlotType(BookSlotType slotType, bool isPopUp)
        {
            model.GetSlotList<BookSlotCompo>(slotType).ForEach(v => v.SetPopUp(isPopUp));
        }
        public void SetLockType(BookLockType lockType, bool isPopUp)
        {
            model._bookSlotList.ForEach(v => v.SetLockSlotPop(lockType, isPopUp));
        }

        private void DescriptionPanelPopUp(Vector2 position, string description)
        {
            model.GetDescriptionPanel().PopUp(model.SetSlotContentAddY(position), description);
        }

        private void SetSlot(BookSlotCompo bookSlot)
        {
            bookSlot.SetLock(false);
            bookSlot.OnShowDescription += DescriptionPanelPopUp;
            bookSlot.OnShowDownDescription += model.GetDescriptionPanel().PopDown;
        }
    }
}