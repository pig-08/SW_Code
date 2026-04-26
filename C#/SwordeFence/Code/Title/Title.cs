using Gwamegi.Code.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SW.Code.Title
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private string scenesName;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private SoundSo sound;
        private int _startIndex = 590;

        private UIDocument _uIPanel;
        private VisualElement _root;

        private List<VisualElement> _swordlList;
        private List<VisualElement> _smallSwordlList;
        private List<Button> _buttonList;


        private void Awake()
        {
            _swordlList = new List<VisualElement>();
            _smallSwordlList = new List<VisualElement>();
            _buttonList = new List<Button>();

            _uIPanel = GetComponent<UIDocument>();
            _root = _uIPanel.rootVisualElement;

            _swordlList.Add(_root.Q<VisualElement>("SwordRed"));
            _swordlList.Add(_root.Q<VisualElement>("SwordBlue"));

            _smallSwordlList.Add(_root.Q<VisualElement>("SmallSwordRed"));
            _smallSwordlList.Add(_root.Q<VisualElement>("SmallSwordBlue"));

            _buttonList.Add(_root.Q<Button>("PlayButton"));
            _buttonList.Add(_root.Q<Button>("ExitButton"));

            foreach (var button in _buttonList)
                button.RegisterCallback<ClickEvent>((v) => StartCoroutine(Click(button, button.name == _buttonList[0].name)));
        }

        private IEnumerator SoundPlay()
        {
            yield return new WaitForSeconds(0.3f);
            soundManager.PlaySound(sound);
        }

        private IEnumerator Click(Button button, bool isPlay)
        {
            for (int i = 0; i < _smallSwordlList.Count; i++)
            {
                int index = _buttonList.IndexOf(button);
                _smallSwordlList[i].style.top = _startIndex + 160 * index;
            }


            _smallSwordlList.ForEach((v) => v.ToggleInClassList("Move"));
            yield return new WaitForSeconds(0.2f);

            if (isPlay)
                Manager.GetCompoManager<TransitionPlayer>().Open(scenesName);
            else
                Application.Quit();
        }


        private void Start()
        {
            PlaySwordAni();
        }

        private async void PlaySwordAni()
        {
            await Awaitable.WaitForSecondsAsync(0.3f);
            _swordlList.ForEach((v) => v.ToggleInClassList("Move"));
            await Awaitable.WaitForSecondsAsync(0.3f);
            soundManager.PlaySound(sound);
        }
    }
}