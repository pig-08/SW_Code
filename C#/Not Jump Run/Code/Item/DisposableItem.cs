using Unity.VisualScripting;
using UnityEngine;

public class DisposableItem : ItemComponent
{
    protected Player _player;
    protected QuickSlotItem _quickSlotItem;

    public override void Init(Player player)
    {
        IsDisposable = true;
        _player = player;
        _quickSlotItem = player.GetCompo<QuickSlotItem>();
    }

    public void PlusCount(int plus)
    {
        ItemCount += plus;
        _quickSlotItem.AllSetText();
    }

    public void MinusCount(int minus)
    {
        if (GetZeroCount())
            ItemCount -= minus;

        _quickSlotItem.AllSetText();
    }
}
