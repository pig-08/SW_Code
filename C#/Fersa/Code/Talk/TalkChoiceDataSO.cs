using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Talk
{
    [CreateAssetMenu(fileName = "", menuName = "SO/Talk/ChoiceData")]
    public class TalkChoiceDataSO : ScriptableObject
    {
        public DirType type;
        public List<ChoiceData> choiceTextList;
    }

    [Serializable]
    public struct ChoiceData
    {
        public string choiceText; //선택지에 나올 텍스트
        public TalkDataListSO nextTalkData; // 선택지를 고르면 나올 대화
        
        public bool isRandom; // 이후에 대화가 랜덤으로 나올것이냐
        public List<TalkDataListSO> randomTalkList; // 이후에 랜덤으로 대화 List
        
        public bool isChangeAction; // 대화 이후 보상을 변경할 것이냐
        public string newActionValue; // 대화 이후 보상뭐로 변경할 것이냐

        public TalkDataListSO GetRandom()
        {
            int index = UnityEngine.Random.Range(0, randomTalkList.Count);
            return randomTalkList[index];
        }
    }
}

