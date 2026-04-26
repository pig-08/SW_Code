using UnityEngine;

public class TempPlaySound : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SoundSo playSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            soundManager.PlaySound(playSound);
    }
}
