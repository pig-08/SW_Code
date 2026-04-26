using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SW.Code.GameOver
{
    public class GameOverUiPlay : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO gameOverEventChannel;
        [SerializeField] private string[] textNameList;
        [SerializeField] private string[] buttonNameList;

        [SerializeField] private SoundManager soundManager;
        [SerializeField] private SoundSo sound;

        private string[] swordNameList = { "Blue", "Red" };

        private UIDocument _document;
        private VisualElement _root;

        private Label _gameOverText;
        private VisualElement _panel;
        private VisualElement[] swordList = new VisualElement[2];
        private Label[] textList = new Label[4];
        private Button[] buttonList = new Button[2];

        GameOverEvent s = new GameOverEvent();
        private void Start()
        {
            _document = GetComponent<UIDocument>();

            _root = _document.rootVisualElement;

            _panel = _root.Q<VisualElement>("Panel");
            _gameOverText = _root.Q<Label>();

            for (int i = 0; i < 2; i++)
                swordList[i] = _root.Q<VisualElement>(swordNameList[i] + "Sword");

            for (int i = 0; i < 4; i++)
                textList[i] = _root.Q<Label>(textNameList[i] + "Text");

            for (int i = 0; i < 2; i++)
                buttonList[i] = _root.Q<Button>(buttonNameList[i] + "Button");

            gameOverEventChannel.AddListener<GameOverEvent>(PlayGameOverUI);

            buttonList[0].RegisterCallback<ClickEvent>((v) => Manager.GetCompoManager<TransitionPlayer>().Open("Main"));
            buttonList[1].RegisterCallback<ClickEvent>((v) => Application.Quit());
        }

        private async void PlayGameOverUI(GameOverEvent evt)
        {
            textList[1].text = "Round : " + evt.tunCount.ToString();
            textList[2].text = "Coin : " + evt.coin.ToString();
            textList[3].text = "SpecialCoin : " + evt.specialCoin.ToString();

            _panel.ToggleInClassList("Move");
            await Awaitable.WaitForSecondsAsync(0.5f);

            _gameOverText.ToggleInClassList("Open");
            await Awaitable.WaitForSecondsAsync(0.5f);

            foreach (var sword in swordList)
                sword.ToggleInClassList("Move");

            await Awaitable.WaitForSecondsAsync(0.3f);
            soundManager.PlaySound(sound);

            foreach(Label text in textList)
            {
                text.ToggleInClassList("Opne");
                await Awaitable.WaitForSecondsAsync(0.1f);
            }

            foreach (Button button in buttonList)
                button.ToggleInClassList("Open");

        }


    }
}