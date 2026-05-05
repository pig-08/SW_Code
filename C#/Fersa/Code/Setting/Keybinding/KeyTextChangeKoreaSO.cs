using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextChangeKoreaSO", menuName = "SO/Keybinding/Korea")]
public class KeyTextChangeKoreaSO : ScriptableObject
{
    public List<KoreaTextData> koreaTextDataList;
    public string GetChangeKoreaText(string text)
    {
        string lower = text?.ToLower();
        foreach (KoreaTextData data in koreaTextDataList)
        {
            if(data.Text.ToLower() == lower)
                return data.KoreaText;
        }

        return text;
    }
}

[Serializable]
public struct KoreaTextData
{
    public string Text;
    public string KoreaText;
}
