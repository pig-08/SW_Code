using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private TextMeshProUGUI colorText;
    [SerializeField] private Slider receptionSlider;

    [Header("Input")]
    [SerializeField] private UIInputSO uIInputSO;
    [SerializeField] private PlayerInputSO playerInputSO;

    [Header("Sound")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider allSoundSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    private Player _player;
    private PlayerRotation _playerRotation;
    private TMP_Dropdown _colorDropdown;
    private Image _dorpImage;


    private bool _isOpneEnd = true;
    private bool _isOpne;

    public void Init(Player player)
    {
        _player = player;
        _playerRotation = player.GetCompo<PlayerRotation>();

        
    }

    private void Start()
    {
        InitDropdown();
        InitReceptionSlider();
        InitSound();
    }

    private void InitSound()
    {
        if (mixer.GetFloat("Master", out float value))
            allSoundSlider.value = value;

        if (mixer.GetFloat("BGM", out float bvalue))
            BGMSlider.value = bvalue;

        if (mixer.GetFloat("SFX", out float svalue))
            SFXSlider.value = svalue;

        allSoundSlider.onValueChanged.AddListener(SetAllSoundSlider);
        BGMSlider.onValueChanged.AddListener(SetBGMSlider);
        SFXSlider.onValueChanged.AddListener(SetSFXSlider);

    }

    private void InitReceptionSlider()
    {
        receptionSlider.value = PlayerPrefs.GetFloat("Reception", 10);
        _playerRotation.SetReception(PlayerPrefs.GetFloat("Reception", 10));
        receptionSlider.onValueChanged.AddListener(SetSliider);
    }

    private void InitDropdown()
    {
        uIInputSO.OnESCPressed += Pop;
        _colorDropdown = GetComponentInChildren<TMP_Dropdown>();
        _dorpImage = _colorDropdown.GetComponent<Image>();
        _colorDropdown.onValueChanged.AddListener(SetColor);
        _colorDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("colorValue", 0));
        SetColor(PlayerPrefs.GetInt("colorValue", 0));
    }

    private async void Pop()
    {
        if (_isOpneEnd == false || _player.GetGameOver() || _player.GetGameEnd()) return;

        _isOpneEnd = false;

        if (_isOpne)
        {
            transform.DOLocalMoveY(-1100f, 0.5f).SetEase(Ease.InOutElastic);
            _isOpne = false;
            playerInputSO.SetPlayerInput(true);
            _playerRotation.SetMouse(false);
            _player.SetSetting(false);
        }
        else
        {
            transform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutElastic);
            _isOpne = true;
            playerInputSO.SetPlayerInput(false);
            _playerRotation.SetMouse(true);
            _player.SetSetting(true);
        }

        await Awaitable.WaitForSecondsAsync(0.5f);
        _isOpneEnd = true;
    }

    private void SetColor(int index)
    {
        if (index == 0)
            _dorpImage.color = Color.black; 
        else
            _dorpImage.color = Color.white;

        colorText.color = _colorDropdown.options[index].color;
        PlayerPrefs.SetInt("colorValue", index);
    }

    private void SetSliider(float value)
    {
        PlayerPrefs.SetFloat("Reception", value);
        _playerRotation.SetReception(value);
    }

    private void SetAllSoundSlider(float value)
    {
        print("³×?");
        mixer.SetFloat("Master",value);
    }
    private void SetBGMSlider(float value)
    {
        mixer.SetFloat("BGM", value);
    }
    private void SetSFXSlider(float value)
    {
        mixer.SetFloat("SFX", value);
    }
}
