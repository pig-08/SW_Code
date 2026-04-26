using UnityEngine;

namespace SW.Code.SO
{
    [CreateAssetMenu(fileName = "StageDataSO", menuName = "SO/StageDataSO")]
    public class StageDataSO : ScriptableObject
    {
        public Sprite stageImage;
        public string stageName;
        public string moveScene;
    }
}