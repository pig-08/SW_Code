using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUiPanel : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private UIInputSO uIInputSO;
    private Player _player;
    private PlayerRotation _playerRotation;
    public void Init(Player player)
    {
        _player = player;
        _playerRotation = _player.GetCompo<PlayerRotation>();
    }

    public void PopUpGameOver()
    {
        transform.DOScale(Vector3.one,0.4f);
        _playerRotation.SetMouse(true);
    }

    public void ReStart()
    {
        _player.PlayerInput.SetPlayerInput(true);
        _playerRotation.SetMouse(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
