using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

namespace SW.Code.Ui
{
    public class GameEndTiemLine : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private GameObject _endPlayer;
        [SerializeField] private List<PlayableAsset> _timeLines;
        private PlayableDirector _playableDirector;
        private bool win;

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        }

        public void OnActive()
        {
            gameObject.SetActive(true);
        }

        public void TimeLineStert(bool isWin)
        {
            if (win) return;
            win = true;
            _camera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = null;
            LensSettings lens = new LensSettings();
            lens.OrthographicSize = 5f;
            lens.NearClipPlane = 0.3f;
            lens.FarClipPlane = 1000;
            lens.Dutch = 0;
            _camera.Lens = lens;
            if(isWin)
                GameManager.Instance.isClearActive = true;
            else
                GameManager.Instance.isOverActive = true;
            _camera.GetComponent<CinemachinePositionComposer>().TargetOffset = new Vector3(isWin ? 7.43f : 0f, 1.84f);
            CameraTarget target = new CameraTarget();
            target.TrackingTarget = _endPlayer.transform;
            _camera.Target = target;
            _playableDirector.Play(isWin ? _timeLines[0] : _timeLines[1]);
        }  


    }
}