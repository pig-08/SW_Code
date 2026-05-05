using System;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.Deck
{
    [CreateAssetMenu(fileName = "DeckSkillDataListSO", menuName = "SO/Data/DeckSkillData")]
    public class DeckSkillDataListSO : ScriptableObject
    {
        public SkillDataSO[] deckInSkills = new SkillDataSO[10];

        public bool IsInSkill(SkillDataSO data)
        {
            foreach(SkillDataSO listData in deckInSkills)
            {
                if (listData == data)
                    return true;
            }
            return false;
        }

        public int GetIndex(SkillDataSO skillData)
        {
            for (int i = 0; i < deckInSkills.Length; i++)
            {
                if (deckInSkills[i] == skillData)
                    return i;
            }

            return -1;
        }

        public void AllDataReSet()
        {
            deckInSkills = new SkillDataSO[10];
            DeckDataSaver.DeckSave(deckInSkills, 0, 0);
            DeckDataSaver.LoadDeck(0);
        }
    }
}