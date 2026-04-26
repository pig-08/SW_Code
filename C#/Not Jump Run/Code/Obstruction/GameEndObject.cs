using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

public class GameEndObject : MonoBehaviour, IObjectComponent
{
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private Transform camTrm;
    [SerializeField] private VisualEffect[] visualEffects;
    [SerializeField] private GameEndPanel gameEndPanel;

    private Player _player;
    private PlayerRotation _playerRotation;
    private bool _isEnd;

    public void Init(Player player)
    {
        _player = player;
        _playerRotation = player.GetCompo<PlayerRotation>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _player.gameObject.layer && _isEnd == false)
        {
            _isEnd = true;

            foreach(VisualEffect effect in visualEffects)
                effect.Play();

            playerCanvas.SetActive(false);
            _player.ChangeState("DANCE");
            _player.GetCompo<PlayerMovement>().StopImmediately();
            _playerRotation.SetMouse(true);
            _player.GameEnd();

            _player.transform.DOLocalMoveX(-53.5f, 0);
            _player.PlayerInput.SetPlayerInput(false);

            _player.transform.localRotation = Quaternion.Euler(new Vector3(0, 180f, 0));
            camTrm.localRotation = Quaternion.Euler(new Vector3(-12f, 0, 0));
            gameEndPanel.End();
        }
    }
}
