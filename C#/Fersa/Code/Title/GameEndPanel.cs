using DG.Tweening;
using UnityEngine;

namespace PSW.Code.Title
{
    public class GameEndPanel : MainUiPopCompo
    {
        private bool _isPopUp;
        private void Awake()
        {
            UiInit();
        }

        public void Pop()
        {
            if (OnPopUpEvent.Invoke(weight, this) == false)
                return;

            if(_isPopUp)
                PopDown();
            else
                PopUp();

            _isPopUp = !_isPopUp;
        }

        public override void PopDown()
        {
            _isPopUp = false;
            base.PopDown();
        }

        public void EndGame()
        {
            Application.Quit();
        }
    }
}