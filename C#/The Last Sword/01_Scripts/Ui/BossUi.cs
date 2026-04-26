using Gwamegi.Code.Entities;
using SW.Code.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Ui
{
    public class BossUi : MonoBehaviour
    {
        [SerializeField] private BossBrain _brain;
        [SerializeField] private EntityFinderSO player;
        private UIDocument dataUiPanel;
        private VisualElement _root;
        private List<VisualElement> _visuals = new List<VisualElement>();
        private Label _bossName;

        public void Init()
        {
            dataUiPanel = GetComponent<UIDocument>();
            _root = dataUiPanel.rootVisualElement;
            _visuals.Add(_root.Q<VisualElement>("Panel"));
            _visuals.Add(_root.Q<VisualElement>("UpPanel"));
            _visuals.Add(_root.Q<VisualElement>("DownPanel"));
            _visuals.Add(_root.Q<VisualElement>("BossImage"));
            _bossName = _root.Q<Label>("Text");
            StartCoroutine(UiStart());
        }

        public IEnumerator UiStart()
        {
            _visuals[1].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[2].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[0].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[3].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _bossName.ToggleInClassList("Move");
            yield return new WaitForSeconds(1f);
            _bossName.ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[3].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[0].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[2].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.2f);
            _visuals[1].ToggleInClassList("Move");
            yield return new WaitForSeconds(0.5f);
            player.target.GetCompo<EntityMover>().CanManualMove = true;
            if (_brain.GetComponent<AgisBossBrain>() != null)
            {
                AgisBossBrain _abrain = _brain.GetComponent<AgisBossBrain>();
                _abrain.AngisInit();
            }
            else
                _brain.Init();
            Destroy(gameObject);
        }
    }
}