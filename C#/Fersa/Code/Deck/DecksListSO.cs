using System;
using System.Collections.Generic;
using UnityEngine;
using YIS.Code.Skills;

namespace PSW.Code.Deck
{
    [CreateAssetMenu(fileName = "DecksListSO", menuName = "SO/Deck/DeckList")]
    public class DecksListSO : ScriptableObject
    {
        public int index;
        public Action<SkillDataSO[]> OnChangeDeck;
        public SkillDataListSO skillDataList;
        public List<DeckSkillDataListSO> deckDataList = new List<DeckSkillDataListSO>();

        public SkillDataSO[] GetCurrentIndexDeckData()
        {
            index = DeckDataSaver.GetCurrentIndex();
            return deckDataList[index].deckInSkills;
        }

        public int GetIndex()
        {
            index = DeckDataSaver.GetCurrentIndex();
            return index;
        }

        public void ChangeIndex(int newIndex)
        {
            if (index != newIndex)
            {
                DeckDataSaver.DeckSave(deckDataList[index].deckInSkills, index, newIndex);
                index = newIndex;
            }

            DeckDataSaver.LoadDeck(index); //이걸로 혹시 Null이면 생성
            List<string> names = DeckDataSaver.LoadDeck(index).skillNameList;

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] != " ")
                    deckDataList[index].deckInSkills[i] = GetSkillData(names[i]);
            }

            OnChangeDeck?.Invoke(deckDataList[index].deckInSkills);
        }

        public int GetIndex(SkillDataSO skillData)
        {
            for (int i = 0; i < GetCurrentIndexDeckData().Length; i++)
            {
                if (GetCurrentIndexDeckData()[i] == skillData)
                    return i;
            }

            return -1;
        }

        private SkillDataSO GetSkillData(string name)
        {
            foreach (SkillDataSO skillData in skillDataList.skills)
            {
                if(skillData.skillName == name)
                    return skillData;
            }

            return null;
        }

        public void AllDataReSet()
        {
            for(int i = 0; i < deckDataList.Count; ++i)
            {
                deckDataList[i].deckInSkills = new SkillDataSO[10];
                DeckDataSaver.DeckSave(deckDataList[i].deckInSkills, i, i);
            }

            DeckDataSaver.DeckSave(new SkillDataSO[10], 0, 0);
            index = 0;

            DeckDataSaver.LoadDeck(index); 
            List<string> names = DeckDataSaver.LoadDeck(index).skillNameList;

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] != " ")
                    deckDataList[index].deckInSkills[i] = GetSkillData(names[i]);
            }

            OnChangeDeck?.Invoke(deckDataList[index].deckInSkills);
        }
    }
}