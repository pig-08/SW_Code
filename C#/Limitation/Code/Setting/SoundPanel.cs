using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPanel : SettingCompo
{
    [SerializeField] private AudioMixer mixer;

    [Header("Slider")]
    [SerializeField] private Slider maxSoundSlider;
    [SerializeField] private Slider BGMSoundSlider;
    [SerializeField] private Slider SFXSoundSlider;

    public override void Init()
    {
        if (mixer.GetFloat("MasterVolume", out float maxValue))
            maxSoundSlider.value = maxValue;

        if (mixer.GetFloat("MusicVolume", out float BGMValue))
            BGMSoundSlider.value = BGMValue;

        if (mixer.GetFloat("SfxVolume", out float SFXValue))
            SFXSoundSlider.value = SFXValue;

        maxSoundSlider.onValueChanged.AddListener(SetMaxVolume);
        BGMSoundSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSoundSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMaxVolume(float value) => mixer.SetFloat("MasterVolume", value);
    public void SetBGMVolume(float value) => mixer.SetFloat("MusicVolume", value);
    public void SetSFXVolume(float value) => mixer.SetFloat("SfxVolume", value);

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
