using SW.Code.SO;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Main
{
    public class StagePanel : MainComponent
    {
        [SerializeField] private Canvas thisCanvas;
        [SerializeField] private StageDataSO[] stageDataList;
        [SerializeField] private Transform content;
        [SerializeField] private GameObject stageListPanelPrefab;

        private Animator _panelAnimator;

        public override void Init(VisualElement root)
        {
            base.Init(root);
            _panelAnimator = GetComponentInChildren<Animator>();

            foreach (StageDataSO stageData in stageDataList)
                Instantiate(stageListPanelPrefab, content).GetComponent<StageListPanel>().Init(stageData);
        }

        public override void Open()
        {
            base.Open();
            _panelAnimator.Play("Open");
        }

        public override void Close()
        {
            base.Close();
            _panelAnimator.Play("Close");
        }

        public void SetCanvasOrder(int order) => thisCanvas.sortingOrder = order;
    }
}