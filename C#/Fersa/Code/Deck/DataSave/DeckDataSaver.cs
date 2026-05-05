using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YIS.Code.Skills;

[Serializable]
public class SaveData
{
    public int currentDeckID;
    public List<DeckSaveData> decks = new List<DeckSaveData>();
}
[Serializable]
public class DeckSaveData
{
    public List<string> skillNameList = new List<string>();
    public int deckID;
}

public static class DeckDataSaver
{
    private static string path;

    private static void SetPath()
    {
        if (path == null)
        {
            path = Path.Combine(Application.persistentDataPath, "database.json");
            Debug.Log(path);
        }
    }

    public static void ReSetData()
    {
        SaveData saveData = LoadAll();

        if (saveData == null)
            return;

        foreach(DeckSaveData data in saveData.decks)
            data.skillNameList.Clear();

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public static DeckSaveData LoadDeck(int deckID)
    {
        SetPath();
        SaveData saveData = LoadAll();

        if (saveData == null)
            return null;

        if (saveData.decks == null)
            saveData.decks = new List<DeckSaveData>();

        if (saveData.decks.Find(d => d.deckID == deckID) == null)
            DeckSave(new SkillDataSO[10], deckID, deckID);

        return saveData.decks.Find(d => d.deckID == deckID);
    }

    private static SaveData LoadAll()
    {
        SetPath();
        
        if (!File.Exists(path))
            return new SaveData();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static int GetCurrentIndex()
    {
        SetPath();

        SaveData saveData = LoadAll();

        if (saveData == null)
            return -1;

        return saveData.currentDeckID;
    }

    public static void DeckSave(SkillDataSO[] skillDatas, int deckID, int currentIndex)
    {
        SetPath();
        SaveData saveData = LoadAll();

        saveData.decks.RemoveAll(d => d.deckID == deckID);

        DeckSaveData deck = new DeckSaveData();
        deck.deckID = deckID;

        foreach (SkillDataSO data in skillDatas)
        {
            if (data != null)
                deck.skillNameList.Add(data.skillName);
            else
                deck.skillNameList.Add(" ");
        }

        saveData.currentDeckID = currentIndex;
        saveData.decks.Add(deck);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }
}
