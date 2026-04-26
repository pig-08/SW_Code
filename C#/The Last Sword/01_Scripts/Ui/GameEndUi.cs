using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Core.StatSystem;
using Gwamegi.Code.Entities;
using Gwamegi.Code.Items;
using Gwamegi.Code.Players;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SW.Code.Ui
{
    public class GameEndUi : MonoBehaviour
    {
        [SerializeField] private EntityFinderSO _entityStat;
        [SerializeField] private List<StatSO> stats;
        [SerializeField] private string nextScene;

        private bool isWin;
        private Animator _animator;

        private UIDocument _panel;
        private VisualElement _root;

        private UnityEngine.UIElements.Label _gameOverText;
        private VisualElement _itemBag;
        private VisualElement _textBag;

        private List<VisualElement> _items;
        private List<UnityEngine.UIElements.Label> _labels;
        private List<Button> _buttons;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            

            _buttons = new List<Button>();
            _items = new List<VisualElement>();
            _labels = new List<UnityEngine.UIElements.Label>();

            _panel = GetComponent<UIDocument>();
            _root = _panel.rootVisualElement;

            _gameOverText = _root.Q<UnityEngine.UIElements.Label>("GameOverText");

            _itemBag = _root.Q<VisualElement>("Items");
            _textBag = _root.Q<VisualElement>("Texts");

            _buttons.Add(_root.Q<Button>("RePlayButton"));
            _buttons.Add(_root.Q<Button>("ExitButton"));

            for (int i = 0; i < 9; i++)
                _items.Add(_itemBag.Q<VisualElement>("Item" + i.ToString()));
            for (int i = 0; i < 6; i++)
                _labels.Add(_textBag.Q<UnityEngine.UIElements.Label>("Text" + i.ToString()));
            foreach (var button in _buttons)
                button.RegisterCallback<ClickEvent>((v) => StartCoroutine(Click(button, button.name == _buttons[0].name)));
            for (int i = 0; i < 9; i++)
                _items[i].style.backgroundImage = new StyleBackground();
        }

        public void GameEndStart(bool win)
        {
            isWin = win;
        }

        private void GameEnd()
        {
            for (int i = 0; i < 6; i++)
                _labels[i].text = $"{stats[i].statName}:{_entityStat.target.GetCompo<EntityStat>().GetStat(stats[i]).Value}";
        }

        public void GameOver() => StartCoroutine(GameOverTime());

        private IEnumerator GameOverTime()
        {
            List<Item> items = _entityStat.target.GetCompo<PlayerItemCompo>().GetItem();
            for (int i = 0; i < items.Count; i++)
                _items[i].style.backgroundImage = new StyleBackground(items[i]._sprite);
            GameEnd(); 
            _gameOverText.text = isWin ? "GameWin" : "GameOver";
            _gameOverText.ToggleInClassList("Create");
            foreach (var label in _labels)
            {
                label.ToggleInClassList("Up");
                yield return new WaitForSeconds(0.2f);
            }

            _itemBag.ToggleInClassList("Open");
            yield return new WaitForSeconds(0.4f);

            foreach (var item in _items)
            {
                item.ToggleInClassList("Move");
                yield return new WaitForSeconds(0.2f);
            }

            foreach (var button in _buttons)
                button.ToggleInClassList("Up");
        }

        private IEnumerator Click(Button button, bool isRePlay)
        {
            print(button.text);
            button.ToggleInClassList("Big");
            yield return new WaitForSeconds(0.15f);
            button.ToggleInClassList("Big");
            yield return new WaitForSeconds(0.2f);
            if (isRePlay)
            {
                SceneManager.LoadScene(nextScene);
            }
            else
                Application.Quit();
        }
    }
}