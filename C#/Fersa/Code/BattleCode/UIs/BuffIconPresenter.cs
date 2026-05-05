using System.Collections.Generic;
using PSB.Code.BattleCode.Events;
using PSB.Code.BattleCode.Players;
using PSW.Code.EventBus;
using UnityEngine;
using YIS.Code.Modules;

namespace PSB.Code.BattleCode.UIs
{
    public class BuffIconPresenter : MonoBehaviour
    {
        [SerializeField] private ModuleOwner myTarget;
        [SerializeField] private Transform iconRoot;
        [SerializeField] private BuffIconView iconPrefab;

        private readonly Dictionary<BuffVisualSO, BuffIconView> _icons = new();

        private void Awake()
        {
            if (iconRoot == null)
                iconRoot = transform;
        }

        private void OnEnable()  => Bus<BuffUiEvent>.OnEvent += OnBuffUiEvent;
        private void OnDisable() => Bus<BuffUiEvent>.OnEvent -= OnBuffUiEvent;

        private void OnBuffUiEvent(BuffUiEvent evt)
        {
            if (evt.Target == null || evt.BuffVisualData == null)
                return;

            if (!ReferenceEquals(evt.Target, myTarget))
                return;

            bool isLeft = myTarget is BattlePlayer;

            switch (evt.Op)
            {
                case BuffUiOp.Applied:
                case BuffUiOp.Updated:
                    Upsert(evt.BuffVisualData, evt.Duration, evt.Target, isLeft);
                    break;

                case BuffUiOp.Removed:
                    Remove(evt.BuffVisualData);
                    break;
            }
        }

        private void Upsert(BuffVisualSO dataSo, int duration, ModuleOwner owner, bool isLeft)
        {
            if (dataSo == null || iconPrefab == null || iconRoot == null)
                return;

            if (!_icons.TryGetValue(dataSo, out var view) || view == null)
            {
                view = Instantiate(iconPrefab, iconRoot);
                _icons[dataSo] = view;
            }

            view.Set(dataSo, duration, owner, isLeft);
        }

        private void Remove(BuffVisualSO dataSo)
        {
            if (dataSo == null) return;

            if (!_icons.TryGetValue(dataSo, out var view) || view == null)
                return;

            Destroy(view.gameObject);
            _icons.Remove(dataSo);
        }

        public void ClearAll()
        {
            foreach (var kv in _icons)
                if (kv.Value != null) Destroy(kv.Value.gameObject);

            _icons.Clear();
        }
        
    }
}
