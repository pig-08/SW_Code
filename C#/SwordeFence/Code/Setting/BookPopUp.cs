using AJ;
using AJ.Scripts;
using DG.Tweening;
using SW.Code.SO;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace SW.Code.Setting
{
    public class BookPopUp : SettingComponent
    {
        [SerializeField] private TurretDataListSO turretDataList;

        [SerializeField] private Transform bookPanel;
        [SerializeField] private Transform content;

        [SerializeField] private GameObject weaponPanel;

        private ScrollRect _scrollRect;
        public override void Init(VisualElement root)
        {
            bookPanel.localScale = Vector2.zero;

            foreach(TurretSO turretData in turretDataList.turretDataList)
                Instantiate(weaponPanel, content).GetComponent<WeaponPanelUi>().Init(turretData);

            _scrollRect = GetComponentInChildren<ScrollRect>();
        }

        IEnumerator ScrollToBottomNextFrame()
        {
            yield return null;
            _scrollRect.verticalNormalizedPosition = 1f;
        }

        public override void SetPanel()
        {
            _isOpen = !_isOpen;
            bookPanel.DOScale(_isOpen ? new Vector2(1,1): Vector2.zero, 0.5f);
            if(_isOpen)
                StartCoroutine(ScrollToBottomNextFrame());
        }
    }
}