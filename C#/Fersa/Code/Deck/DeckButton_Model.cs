using PSW.Code.Deck;
using System;
using UnityEngine;

public class DeckButton_Model : ModelCompo<int>
{
    [field: SerializeField] public DecksListSO DeckDataList { private set; get; }
    [SerializeField] private SpriteData popData;

    private DeckEvnet _deckEvnet;

    public Sprite GetPopData(bool isUp) => popData.GetSprite(isUp);

    public override int InitModel()
    {
        return _currentData; 
    }
    public void SetIndex(int index) { _deckEvnet.index = index; }
    public DeckEvnet GetDeckEvnet() => _deckEvnet;

}

[Serializable]
public struct SpriteData
{
    public Sprite Up;
    public Sprite Down;

    public Sprite GetSprite(bool isOne)
    {
        if (isOne)
            return Up;
        else
            return Down;
    }
}
