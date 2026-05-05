using PSB.Code.BattleCode.Events;
using PSW.Code.EventBus;
using UnityEngine;
using Work.PSB.Code.CoreSystem;

namespace Work.PSW.Code.Title
{
    public class TitleButton : MonoBehaviour
    {
        [SerializeField] private TransitionController transition;

        private int _saveKey = 0;

        private void Start()
        {
            _saveKey = PlayerPrefs.GetInt("Stage");
            Debug.Log(_saveKey);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.DeleteKey("Stage");
            }
        }

        public void ButtonClick()
        {
            if (_saveKey != 1)
            {
                Bus<FirstTutoEvent>.Raise(new FirstTutoEvent());
            }
            else
            {
                transition.nextScene = "PSB_Field";
                transition.Transition();
            }
        }
        
    }
}
