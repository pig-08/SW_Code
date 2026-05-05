using PSW.Code.Deck;
using UnityEngine;

public class DeckButtonPanel_Model : ModelCompo<object>
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform parentTrm;

    [SerializeField] private int notMoveIndex = 5;
    [SerializeField] private float oneMoveValue = 50f;

    [field: SerializeField] public DecksListSO DeckDataList { private set; get; }
    private DeckButton_Controller _currentDeckButton;

    public override object InitModel()
    {
        return _currentData;
    }

    public void SetButton(DeckButton_Controller deckButton) => _currentDeckButton = deckButton;
    public DeckButton_Controller GetDeckButton() => _currentDeckButton;
    public GameObject GetButtonPrefab() => buttonPrefab;
    public Transform GetParentTrm() => parentTrm;
    public float GetMoveCurrentValue(int index)
    {
        int currentIndex = index - notMoveIndex;

        if (currentIndex <= 0)
            return 0;

        return currentIndex * oneMoveValue;
    }
}
