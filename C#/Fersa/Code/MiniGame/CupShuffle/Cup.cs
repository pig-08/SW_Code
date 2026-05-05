using PSB.Code.CoreSystem.Events;
using PSW.Code.EventBus;
using PSW.Code.Talk;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace PSW.MiniGame.CupShuffle
{
    public class Cup : TalkEntity
    {
        [SerializeField] private TalkEntity box;
        [SerializeField] private string[] offTextList = new string[3];
        [SerializeField] private string onText;

        private void Start()
        {
            base.Start();
            SetBox(false);
            BoxSetTalkEnabled(false);
            SetTalkEnabled(false);
            Bus<TalkFinished>.OnEvent += OnValueBox;
        }

        private void OnDestroy()
        {
            Bus<TalkFinished>.OnEvent -= OnValueBox;
        }

        public void SetBox(bool isOn) => box.gameObject.SetActive(isOn);
        public void BoxSetTalkEnabled(bool isOn) => box.SetTalkEnabled(isOn);

        public void SetUpInteractData()
        {
            BoxSetTalkEnabled(true);
            Action.Setup(BuildContext());
        }

        private async void OnValueBox(TalkFinished evt)
        {
            if(onText == evt.RewardKey)
            {
                await Awaitable.FixedUpdateAsync();
                Bus<ShuffleGameEnd>.Raise(new ShuffleGameEnd());
                SetTalkEnabled(true);
                return;
            }

            foreach(string key in offTextList)
            {
                if (key == evt.RewardKey)
                {
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}