using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Items : MonoBehaviour, IPlayerComponent
{
    public event Action OnItemIndexChange;

    private List<ItemComponent> _itemList = new List<ItemComponent>();
    private Player _player;

    private ItemComponent _currentItme;

    public void Init(Player player)
    {
        _player = player;
        GetIItemComponents();
        ComponentsInit();
    }

    public void PickUpItem(ItemComponent item)
    {
        _currentItme?.PickDown();
        _currentItme = item;
        _currentItme?.PickUp();
    }

    public void NotItem()
    {
        _currentItme?.PickDown();
        _currentItme = null;
    }

    private void GetIItemComponents()
    {
        GetComponentsInChildren<ItemComponent>().ToList().ForEach(v => _itemList.Add(v));
    }

    private void ComponentsInit()
    {
        _itemList.ForEach(v => v.Init(_player));
    }

    public T GetComp<T>() where T : ItemComponent
    {
        return _itemList.OfType<T>().FirstOrDefault();
    }

    public List<ItemComponent> GetItemComponentList()
    {
        return _itemList;
    }

    public void ChangeItem(int index1, int index2)
    {
        ItemComponent tempItem = _itemList[index1];
        _itemList[index1] = _itemList[index2];
        _itemList[index2] = tempItem;
        OnItemIndexChange?.Invoke();
    }
}
