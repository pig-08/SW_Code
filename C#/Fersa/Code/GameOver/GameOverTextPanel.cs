using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace PSW.Code.GameOver
{
    public class GameOverTextPanel : MonoBehaviour
    {
        [SerializeField] private List<TextData> textDataList;
       
        private Dictionary<GameOverTextType, TextMeshProUGUI> _gameOverTextDic = new Dictionary<GameOverTextType, TextMeshProUGUI>();
        private Dictionary<GameOverTextType, string> _gameOverBaseTextDic = new Dictionary<GameOverTextType, string>();

        private void Awake()
        {
            List<TextMeshProUGUI> gameOverTextList = GetComponentsInChildren<TextMeshProUGUI>().ToList();

            for (int i = 0; i < gameOverTextList.Count; i++)
                _gameOverTextDic.Add((GameOverTextType)i, gameOverTextList[i]);

            for (int i = 0; i < textDataList.Count; i++)
                _gameOverBaseTextDic.Add(textDataList[i].type, textDataList[i].text);
        }

        public void SetGameOverText(GameOverTextType type, string text)
        {
            if(_gameOverTextDic.TryGetValue(type, out TextMeshProUGUI currentText)
                && _gameOverBaseTextDic.TryGetValue(type, out string baseText))
                currentText.SetText(string.Format(baseText, text));
        }

        public void SetTimeText(int time)
        {
            int hour = time / 3600;
            time %= 3600;
            int minute = time / 60;
            time %= 60;

            if (_gameOverTextDic.TryGetValue(GameOverTextType.Time, out TextMeshProUGUI currentText))
                currentText.SetText($"플레이 시간 : {hour}h {minute}m {time}s");
        }
    }

    public enum GameOverTextType
    {
        Time,
        Coin,
        BossCoin,
        PP,
        Enemy,
        Attack,
        Defense,
        HP
    }

    [Serializable]
    public struct TextData
    {
        public GameOverTextType type;
        public string text;
    }

}