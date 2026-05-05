using PSW.Code.Book;
using UnityEngine;
using UnityEngine.EventSystems;
using YIS.Code.Items;
using YIS.Code.Skills;

public class BookItemSlot : BookSlotCompo
{
    public override void Init<T>(T data)
    {
        base.Init(data);
        ItemDataSO itemData = data as ItemDataSO;
        
        if (itemData == null)
            return;

        _description = itemData.visualData.itemDescription;
        icon.sprite = itemData.visualData.icon;
    }

    public override void SetLock(bool isLock)
    {
        base.SetLock(isLock);
        icon.color = isLock ? Color.black : Color.white;
    }
}
