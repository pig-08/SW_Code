using UnityEngine;

public class PlayerRotation : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Transform camTrm;

    private Player _player;
    private float _reception;

    private float mouseX;
    private float yRotation;

    private float mouseY;
    private float xRotation;


    public void Init(Player player)
    {
        _player = player;
        SetMouse(false);

        xRotation = camTrm.localRotation.x;
        yRotation = _player.transform.localRotation.y;
    }

    public void SetMouse(bool isMouse)
    {
        if(isMouse)
            Cursor.lockState = CursorLockMode.None;
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (_player.GetGameEnd()) return;

        mouseX = -_player.PlayerInput.MouseKey.x * _reception * Time.deltaTime;
        mouseY = _player.PlayerInput.MouseKey.y * _reception * Time.deltaTime;

        yRotation -= mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -25f, 42f);

        camTrm.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        _player.transform.localRotation = Quaternion.Euler(0, yRotation, 0f);
    }

    public void SetReception(float reception)
    {
        _reception = reception;
    }
}
