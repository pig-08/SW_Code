using AJ;
using Gwamegi.Code.Input;
using Gwamegi.Code.Managers;
using Gwamegi.Code.TowerCreate;
using SW.Code.Setting;
using SW.Code.Stat;
using SW.Code.UpGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace SW.Code.Managers
{
    public class UpGradeMangers : MonoBehaviour, IManager
    {
        [SerializeField] private GameInputSO gameInputSO;

        private Manager _manager;
        private UpGradePanel _upGrade;

        private List<UpGradeComponent> _upGradeComponents = new List<UpGradeComponent>();

        private UIDocument _uiPanel;
        private VisualElement _root;

        public Tower currentTower;

        public TowerAttackRangeArea area;

        public void Initailze(Manager manager)
        {
            _manager = manager;

            _uiPanel = GetComponent<UIDocument>();
            _root = _uiPanel.rootVisualElement;

            gameInputSO.OnMouseClickEvent += HandleClickEvent;
        }

        public void OnDestroy()
        {
            gameInputSO.OnMouseClickEvent -= HandleClickEvent;

        }

        private void Start()
        {
            _upGrade = GetComponentInChildren<UpGradePanel>();
            _upGrade.Init(_root);
        }

        private void HandleClickEvent(Vector3 position)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(position);
            currentTower = Manager.GetCompoManager<CreateTowerManager>().GetPositionWithTower(pos);

            if (currentTower != null)
            {
                Open(currentTower.GetTurretData(), currentTower.GetTowerCompo<TurretStat>());
                area.SetAttackRangeArea(Manager.GetCompoManager<CreateTowerManager>().GetCellCenterPosition(pos), currentTower.GetTowerCompo<TurretStat>().GetStat("AttackRange").Value);
            }
        }

        public void Open(TurretSO turretData, TurretStat turretStat)
        {
            if (_upGrade.GetAllOpen() == false)
                _upGrade.Open(turretData, turretStat);
        }   

        public void Close()
        {
            area.SetAttackRangeArea(Vector3.zero, 0,false);
        }
    }
    
}

public enum UpGradepType
{
    WeaponSpeed,
    AttackDamage,
    CreateSpeed,
    WeaponSize,
    AttackRange,
    GetGainGold,
    CriticalDMG,
    CriticalChance,
    MAX
}