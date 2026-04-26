using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bag : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private UIInputSO uiInputSO;

    private List<BagItemSlot> _bagItemSlotList = new List<BagItemSlot>();

    private Player _player;
    private Items _items;
    private PlayerRotation _playerRotation;

    private bool _isPop;
    private bool _isEndPop = true;
    private bool _isChange;

    private int _changeIndex = -1;

    public void Init(Player player)
    {
        _player = player;
        uiInputSO.OnTapPressed += Pop;
    }

    private void OnDisable()
    {
        uiInputSO.OnTapPressed -= Pop;
        _items.OnItemIndexChange -= IconChange;
    }

    private void Start()
    {
        _items = _player.GetCompo<Items>();
        _playerRotation = _player.GetCompo<PlayerRotation>();
        _items.OnItemIndexChange += IconChange;
        _bagItemSlotList = GetComponentsInChildren<BagItemSlot>().ToList();

        foreach(BagItemSlot bagSlot in _bagItemSlotList)
            bagSlot.Init();

        IconChange();
    }

    private void Update()
    {
        if (_isPop && _player.GetSetting())
            Pop();
    }
    private void IconChange()
    {
        List<ItemComponent> itemList = _items.GetItemComponentList();
        for (int i = 0; i < _bagItemSlotList.Count; ++i)
        {
            if (i < itemList.Count)
                _bagItemSlotList[i].SetIcon(itemList[i]);
        }
    }

    private void Pop()
    {
        if (_isEndPop == false || _player.GetSetting() || _player.GetGameOver() || _player.GetGameEnd()) return;

        foreach (BagItemSlot bagSlot in _bagItemSlotList)
            bagSlot.SetText();

        if (_isPop)
            PopDown();
        else
            PopUp();
    }

    private async void PopUp()
    {
        _isEndPop = false;

        _playerRotation.SetMouse(true);
        _player.PlayerInput.SetPlayerInput(false);

        transform.DOMoveY(0,0.4f).SetEase(Ease.InOutElastic);
        await Awaitable.WaitForSecondsAsync(0.4f, destroyCancellationToken);

        _isPop = true;
        _isEndPop = true;
    }

    private async void PopDown()
    {
        _isEndPop = false;

        _playerRotation.SetMouse(false);
        _player.PlayerInput.SetPlayerInput(true);

        transform.DOMoveY(-200, 0.4f).SetEase(Ease.InOutElastic);
        await Awaitable.WaitForSecondsAsync(0.4f, destroyCancellationToken);

        _isPop = false;
        _isEndPop = true;
    }

    public void Change(int index)
    {
        if(_isChange == false)
        {
            _isChange = true;
            _changeIndex = index;
        }
        else
        {
            if (_changeIndex != index)
                _items.ChangeItem(_changeIndex, index);

            foreach (BagItemSlot slot in _bagItemSlotList)
                slot.ChangeOff();

            _isChange = false;
            _changeIndex = -1;
        }
    }
}
