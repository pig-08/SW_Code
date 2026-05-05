using Gwamegi.Code.Managers;
using SW.Code.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace SW.Code.Main
{
    public class MainUi : MonoBehaviour
    {
        [SerializeField] private UiInputSO inputSO;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private SoundSo sound;

        private string[] _textNameList = { "Stage", "Setting", "Tutorial", "Title", "Exit" , "Up" , "Down"};
        private UIDocument _uiPanel;

        private VisualElement _root;
        private Label[] _textList = new Label[7];

        private Label notDoText;

        private bool isScroll = true;

        private int textIndex = 2;

        private Dictionary<int, MainComponent> _componentList = new Dictionary<int, MainComponent>();
        private MainComponent currentPanel;


        private int TextIndex
        {
            get
            {
                return textIndex;
            }
            set
            {
                textIndex = value;
                if (textIndex >= _textNameList.Length - 2)
                    textIndex = 0;
                else if (textIndex < 0)
                    textIndex = _textList.Length - 3;
            }
        }

        private int index = 3;
        private int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                if (index > _textNameList.Length - 2)
                    index = 1;
                else if (index <= 0)
                {
                    index = _textNameList.Length - 2;
                }
            }
        }
        private void Awake()
        {
            _uiPanel = GetComponent<UIDocument>();
            _root = _uiPanel.rootVisualElement;

            for(int i = 0; i < _textList.Length; ++i)
                _textList[i] = _root.Q<Label>(_textNameList[i] + "Text");

            GetMainComponent();
            InitMainComponent();

            SetPanel();
        }

        private void Start()
        {
            soundManager = Manager.GetCompoManager<SoundManager>();
        }
        private void Update()
        {
            if(inputSO.UpDownValue != 0 && isScroll)
                SetText(inputSO.UpDownValue == -1);
        }

        private async void SetText(bool isPlus)
        {
            isScroll = false;

            SetUpDownPanel(isPlus);

            for (int i = 0; i < _textNameList.Length - 2; ++i)
            {
                _textList[TextIndex].ToggleInClassList("Number" + (isPlus ? Index++ : Index--));
                _textList[TextIndex].ToggleInClassList("Number" + Index);

                TextIndex = isPlus ? TextIndex + 1 : TextIndex - 1;
            }

            TextIndex = isPlus ? TextIndex - 1 : TextIndex + 1;

            SetPanel();
            await Awaitable.WaitForSecondsAsync(0.22f);

            notDoText.ToggleInClassList("NotDo");
            isScroll = true;
        }

        private async void SetPanel()
        {
            if (_componentList.TryGetValue(TextIndex, out MainComponent compo))
            {
                bool isState = compo.GetComponent<StagePanel>() != null;

                if (isState)
                {
                    StagePanel stage = (StagePanel)compo;
                    stage.SetCanvasOrder(2);
                }

                isState = currentPanel?.GetComponent<StagePanel>() != null;

                if (isState)
                {
                    StagePanel stage = (StagePanel)currentPanel;
                    stage.SetCanvasOrder(-2);
                }

                soundManager.PlaySound(sound);
                compo?.Open(); //sdsdsd
                await Awaitable.WaitForSecondsAsync(0.2f);
                currentPanel?.Close();
                currentPanel = compo;
            }
        }

        private async void SetUpDownPanel(bool isPlus)
        {
            if (isPlus)
            {
                _textList[5].text = _textList[GetIndex(2)].text;
                _textList[6].text = _textList[GetIndex(1)].text;

                notDoText = _textList[GetIndex(2)];
            }
            else
            {
                _textList[5].text = _textList[GetIndex(-1)].text;
                _textList[6].text = _textList[GetIndex(3)].text;

                notDoText = _textList[GetIndex(3)];
            }

            notDoText.ToggleInClassList("NotDo");

            int toIndex = isPlus ? 5 : 6;
            int setIndex = isPlus ? 6 : 5;

            string toText = isPlus ? "ToDown" : "ToUp";

            _textList[toIndex].ToggleInClassList("TextUp");

            await Awaitable.WaitForSecondsAsync(0.001f);

            _textList[setIndex].ToggleInClassList("SetUp");
            _textList[toIndex].ToggleInClassList(toText);

            await Awaitable.WaitForSecondsAsync(0.2f);

            _textList[toIndex].ToggleInClassList("TextUp");
            _textList[toIndex].ToggleInClassList(toText);

            _textList[setIndex].ToggleInClassList("SetUp");
        }
        private int GetIndex(int plus)
        {
            int tempindex = TextIndex;

            tempindex += plus;
            if (tempindex >= _textNameList.Length - 2)
                tempindex -= (_textNameList.Length - 2);
            else if (tempindex < 0)
                tempindex = _textList.Length - 3;

            return tempindex;
        }
        private void GetMainComponent()
        {
            GetComponentsInChildren<MainComponent>().ToList()
                .ForEach(v => _componentList.Add(v.GetIndex(), v));
        }
        private void InitMainComponent()
        {
            _componentList.Values.ToList().ForEach(v => v.Init(_root));
        }
    }
}