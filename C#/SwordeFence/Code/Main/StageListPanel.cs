using Gwamegi.Code.Managers;
using SW.Code.SO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SW.Code.Main
{
    public class StageListPanel : MonoBehaviour
    {
        [SerializeField] private Image stageImage;
        [SerializeField] private TextMeshProUGUI stageNameText;

        private StagePanel _stagePanel;
        private string _moveSceneName;

        public void Init(StageDataSO stageData)
        {
            stageImage.sprite = stageData.stageImage;
            stageNameText.text = stageData.stageName;
            _moveSceneName = stageData.moveScene;
        }

        public void MoveScene() => Manager.GetCompoManager<TransitionPlayer>().Open(_moveSceneName);
    }
}