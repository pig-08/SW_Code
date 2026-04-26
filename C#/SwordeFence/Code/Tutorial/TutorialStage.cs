using DG.Tweening;
using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Input;
using Gwamegi.Code.Managers;
using Gwamegi.Code.Maps;
using Gwamegi.Code.TowerCreate;
using SW.Code.SO;
using SW.Code.UpGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace SW.Code.Tutorial
{
    public class TutorialStage : MonoBehaviour
    {
        [Header("클래스")]
        [SerializeField] private CheckerMover checkerMover;
        [SerializeField] private TutorialUiPlayer tutorialUiPlayer;
        [SerializeField] private UiInputSO uiInputSO;

        [Header("이벤트")]
        [SerializeField] private GameEventChannelSO _mapEventChannel;

        [Header("기초값")]
        [SerializeField] private string[] tutorialTextList;
        [SerializeField] private float TextDelay;

        [Header("구간")]
        [SerializeField] private int[] sectionList;
        [SerializeField] private int[] arrowIndexList;
        [SerializeField] private Vector2[] arrowPointList;
        [SerializeField] private Quaternion[] arrowRotationList;

        private Dictionary<int, Vector2> _arrowValueDictionary = new Dictionary<int, Vector2>();

        private CreateTowerManager _createTowerManager;

        //인덱스
        private int endIndex;
        private int textIndex;
        private int sectionIndex;

        private float textDelay;
        private bool isTextSklip;

        //텍스트 구간 확인
        private bool isTurretEnd;
        private bool isMoveTextEnd;

        private void Start()
        {
            textDelay = TextDelay;

            endIndex = tutorialTextList.Length;

            _mapEventChannel.AddListener<EndPositionCheckEvent>(HandleMoveEnd);
            checkerMover.SetIsMove(false);

            _createTowerManager = Manager.GetCompoManager<CreateTowerManager>();

            for (int i = 0; i < arrowIndexList.Length; ++i)
                _arrowValueDictionary.Add(arrowIndexList[i], arrowPointList[i]);

            uiInputSO.OnMousePressed += TextOn;

            tutorialUiPlayer.UpPanel();
            SetTutorialText();
        }

        private void OnDisable()
        {
            uiInputSO.OnMousePressed -= TextOn;
            _mapEventChannel.RemoveListener<EndPositionCheckEvent>(HandleMoveEnd);
        }

        private void Update()
        {
            
        }

        private void TextOn()
        {
            
            if (isTextSklip == false)
            {
                isTextSklip = true;
                textDelay = TextDelay;
                SetTutorialText();
            }
            else
                textDelay = 0;
        }

        private async void SetTutorialText()
        {
            
            tutorialUiPlayer.SetArrowOnOff(false);

            if (_arrowValueDictionary.TryGetValue(textIndex, out Vector2 point))
            {
                tutorialUiPlayer.SetArrowOnOff(true);
                tutorialUiPlayer.SetUpArrow(point, arrowRotationList[arrowPointList.ToList().IndexOf(point)]);
            }
            print(sectionList.Length);

            if (textIndex == sectionList[sectionIndex] && sectionList.Length > sectionIndex)
            {
                SectionSet(sectionIndex++);
                return;
            }

            tutorialUiPlayer.ClearText();

            if (endIndex <= textIndex)
            {
                Manager.GetCompoManager<TransitionPlayer>().Open("Main");
                tutorialUiPlayer.DownPanel();
                return;
            }

            foreach (char text in tutorialTextList[textIndex])
            {
                tutorialUiPlayer.SetText(text);
                await Awaitable.WaitForSecondsAsync(textDelay);
            }

            isTextSklip = false;
            textIndex++;
        }

        private void HandleMoveEnd(EndPositionCheckEvent evt)
        {
            isMoveTextEnd = true;
            tutorialUiPlayer.UpPanel();
            SetTutorialText();

            
        }

        private void SectionSet(int index)
        {
            tutorialUiPlayer.DownPanel();
            switch (index)
            {
                case 0:
                    {
                        checkerMover.SetIsMove(true);
                        break;
                    }
            }
        }
    }
}