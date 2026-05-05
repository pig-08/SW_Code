using DG.Tweening;
using PSW.Code.BaseSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSW.Code.MainUi
{
    public class MainUiManager : MonoBehaviour
    {
        private BaseOnOffSystemUI _currentFieldUi;

        private void Awake()
        {
            GetComponentsInChildren<BaseOnOffSystemUI>().ToList().ForEach(ui => 
            {
                ui.OnPopUpEvent += IsPopUp;
            });
        }

        private void OnDestroy()
        {
            GetComponentsInChildren<BaseOnOffSystemUI>().ToList().ForEach(ui =>
            {
                ui.OnPopUpEvent -= IsPopUp;
            });
        }

        private bool IsPopUp(int weight, BaseOnOffSystemUI newFieldUi)
        {
            if (_currentFieldUi == newFieldUi)
                newFieldUi = null;
            else if (_currentFieldUi?.IsHighWeight(weight) == false)
                return false;

            _currentFieldUi?.PopDown();
            _currentFieldUi = newFieldUi;

            return _currentFieldUi != null;
        }
    }
}