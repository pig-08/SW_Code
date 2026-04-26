using UnityEngine;

public class SoundSettingCanvasSet : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private bool _isOn;
    private void Awake()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            UiPop();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UiPop()
    {
        panel.SetActive(!_isOn);
        if (_isOn)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        _isOn = !_isOn;
    }
}
