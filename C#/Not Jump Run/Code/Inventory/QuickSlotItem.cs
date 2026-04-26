using System;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotItem : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private GameObject ItemSlotPrefab;

    private Player _player;
    
    private ItemComponent[] _quickItem = new ItemComponent[4];
    private List<ItemSlot> _slots = new List<ItemSlot>();
    private Items _items;

    private ItemSlot _currentSlot;

    public void Init(Player player)
    {
        _player = player;
        _player.PlayerInput.OnIntKeyPressed += Pick;
    }

    private void OnDisable()
    {
        _player.PlayerInput.OnIntKeyPressed -= Pick;
        _items.OnItemIndexChange -= SetIcon;
    }

    private void Start()
    {
        _items = _player.GetCompo<Items>();
        _items.OnItemIndexChange += SetIcon;
        List<ItemComponent> items = _items.GetItemComponentList();
        for (int i = 0; i < (items.Count > 4 ? 4 : items.Count); ++i)
            _quickItem[i] = items[i];

        for (int i = 0; i < _quickItem.Length; ++i)
        {
            ItemSlot item = Instantiate(ItemSlotPrefab, transform).GetComponent<ItemSlot>();
            _slots.Add(item);
        }
        SetIcon();
    }

    private void SetIcon()
    {
        List<ItemComponent> items = _items.GetItemComponentList();
        for (int i = 0; i < (items.Count > 4 ? 4 : items.Count); ++i)
            _quickItem[i] = items[i];

        for (int i = 0; i < _quickItem.Length; ++i)
        {
            _slots[i].SetIcon(_quickItem[i]);
        }
    }

    public void Pick(int index)
    {
        if (_currentSlot == _slots[index])
            NotItem();
        else
            PickUp(index);
    }

    public void PickUp(int index)
    {
        _currentSlot?.PickDown();
        _currentSlot = _slots[index];
        _currentSlot?.PickUp();
        _items.PickUpItem(_quickItem[index]);
    }

    public void NotItem()
    {
        _currentSlot?.PickDown();
        _currentSlot = null;
        _items.NotItem();
    }

    public void AllSetText()
    {
        foreach (ItemSlot slot in _slots)
            slot?.SetText();
    }
}
