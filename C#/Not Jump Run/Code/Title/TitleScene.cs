using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private PlayerInputSO inputSO;

    private void Awake()
    {
        inputSO.SetPlayerInput(false);
    }
}
