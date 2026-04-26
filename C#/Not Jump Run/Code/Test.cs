using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private SoundSo soundSo;

    private SoundManager _soundManager;
    private void Start()
    {
        _soundManager = SoundManager.sound;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            //_soundManager.PlaySound(soundSo);
        }
    }
}
