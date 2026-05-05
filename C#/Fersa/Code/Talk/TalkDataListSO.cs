using PSW.Code.Talk;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkList", menuName = "SO/Talk/DataList", order = 1)]
public class TalkDataListSO : ScriptableObject
{
    [Header("CharacterData")]
    public CharacterData leftCharacter;
    public CharacterData rightCharacter;

    [Header("TalkValueData")]
    public List<TalkValueData> talkDataList;
}

[Serializable]
public struct CharacterData
{
    [TextArea]
    public string Name; // 캐릭터 이름
    public Sprite CharacterImgae; //캐릭터 이미지
    public DirType GazeDirType; //이미지가 보고있는 방향
}

[Serializable]
public struct TalkValueData
{
    public DirType Type; //말하는 캐릭터가 어디에 있는 캐릭터인지
    [TextArea]
    public string Text;
    public TalkChoiceDataSO choiceData;
}

public enum DirType
{
    Left,
    Right,
    End
}
