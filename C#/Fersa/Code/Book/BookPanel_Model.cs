using PSB.Code.BattleCode.UIs.BossShopUI;
using PSB.Code.CoreSystem.SaveSystem;
using PSB.Code.CoreSystem.SaveSystem.BossShop;
using PSB_Lib.Dependencies;
using PSW.Code.Book;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YIS.Code.Items;
using YIS.Code.Skills;

public class BookPanel_Model : ModelCompo<BookSlotCompo>
{
    [Header("Transform")]
    [SerializeField] private Transform content;
    [SerializeField] private Transform toggleContent;

    [Header("Slot")]
    [SerializeField] private GameObject skillSlotPrefab;
    [SerializeField] private GameObject itemSlotPrefab;

    [Header("Toggle")]
    [SerializeField] private GameObject slotTogglePrefab;
    [SerializeField] private GameObject lockTogglePrefab;

    [field :SerializeField] public SkillDataListSO playerSkillList {get; private set;}
    [field :SerializeField] public ItemDatabase itemDataListSO {get; private set;}
    [field :SerializeField] public BossItemListSO lockDataListSO { get; private set; }

    [Inject] private BossUnlockRepository _unlockRepository;

    private Dictionary<SkillDataSO, BookSkillSlot> _skillSlotDic = new Dictionary<SkillDataSO, BookSkillSlot>();
    private Dictionary<ItemDataSO, BookItemSlot> _itemSlotDic = new Dictionary<ItemDataSO, BookItemSlot>();
    public List<BookSlotCompo> _bookSlotList { get; private set; } =  new List<BookSlotCompo>();

    private DescriptionPanel _descriptionPanel;

    public override BookSlotCompo InitModel()
    {
        _descriptionPanel = GetComponentInChildren<DescriptionPanel>();
        return _currentData;
    }

    public DescriptionPanel GetDescriptionPanel() => _descriptionPanel;

    public Vector2 SetSlotContentAddY(Vector2 position)
    {
        position.y += content.localPosition.y;
        return position;
    }
    public T InstantiateObejectAndGetComponent<T>(GameObject prefab, Transform parent) where T : Component
    {
        return Instantiate(prefab, parent).GetComponent<T>();
    }
    public T NewSlotAndGetComponent<T>(BookSlotType slotType) where T : Component
    {
        switch (slotType)
        {
            case BookSlotType.Skill:
                return InstantiateObejectAndGetComponent<T>(skillSlotPrefab, content);
            case BookSlotType.Item:
                return InstantiateObejectAndGetComponent<T>(itemSlotPrefab, content);
            default:
                return null;
        }
    }

    public TagToggleCompo<T> NewToggleAndGetComponent<T>(T type)
    {
        GameObject temp = type is BookSlotType ? slotTogglePrefab : lockTogglePrefab;
        return InstantiateObejectAndGetComponent<TagToggleCompo<T>>(temp, toggleContent);
    }

    public List<BookSkillSlot> GetSkillDicList()
    {
        var sortedPairs = _skillSlotDic.Values.Zip(_skillSlotDic.Keys, (slot, grade) => new { slot, grade.grade })
                               .OrderBy(pair => pair.grade)
                               .ToList();

        List<BookSkillSlot> sortedSlots = new List<BookSkillSlot>();
        sortedPairs.ForEach(pair => sortedSlots.Add(pair.slot));

        return sortedSlots;
    }

    public bool IsUnLock(int id) => _unlockRepository.IsUnlocked(id);

    public BookSkillSlot GetSkillSlot(SkillDataSO skillData)
    {
        if (_skillSlotDic.TryGetValue(skillData, out BookSkillSlot skillSlot))
            return skillSlot;
        return null;
    }

    public BookItemSlot GetItemSlot(ItemDataSO itemData)
    {
        if (_itemSlotDic.TryGetValue(itemData, out BookItemSlot itemSlot))
            return itemSlot;
        return null;
    }

    public List<T> GetSlotList<T>(BookSlotType slotType) where T : BookSlotCompo
    {
        List<T> slotList = new List<T>();
        switch (slotType)
        {
            case BookSlotType.Skill:
                foreach (var slot in _skillSlotDic.Values)
                    slotList.Add(slot as T);
                break;
            case BookSlotType.Item:
                foreach (var slot in _itemSlotDic.Values)
                    slotList.Add(slot as T);
                break;
        }
        return slotList;
    }

    public void AddSkillDic(SkillDataSO skillData, BookSkillSlot skillSlot)
    {
        _skillSlotDic.Add(skillData, skillSlot);
        _bookSlotList.Add(skillSlot);
    }
    public void AddItemDic(ItemDataSO itemData, BookItemSlot itemSlot)
    {
        _itemSlotDic.Add(itemData, itemSlot);
        _bookSlotList.Add(itemSlot);
    }
}
