using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace PSW.Code.Setting
{
    public class SoundPanel : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private List<VolumeData> volumeTextList;
        private void Awake()
        {
            foreach (VolumeData text in volumeTextList)
                text.Init(_mixer);
        }

        private void OnDestroy()
        {
            foreach (VolumeData text in volumeTextList)
                text.VolumeSave();
        }
    }

    [Serializable]
    public struct VolumeData
    {
        public AudioMixerGroup Group;
        public TextMeshProUGUI Text;
        public Slider VolumeSlider;

        private AudioMixer _mixer;

        public void Init(AudioMixer mixer)
        {
            _mixer = mixer;
            VolumeSlider.onValueChanged.AddListener(SetVolume);
            SetSliderValue();
        }

        public void SetSliderValue()
        {
            float value = PlayerPrefs.GetFloat(Group.name);
            VolumeSlider.value = value;
            SetVolume(value);
        }

        public void SetVolume(float value)
        {
            _mixer.SetFloat(Group.name, value);
            SetText(value);
        }

        private void SetText(float value)
        {
            Text.text = ConvertValue(value).ToString() + '%';
        }

        private int ConvertValue(float input)
        {
            float minInput = -80f;
            float maxInput = 0f;
            float minOutput = 0f;
            float maxOutput = 100f;

            float normalized = (input - minInput) / (maxInput - minInput);
            float result = normalized * (maxOutput - minOutput) + minOutput;

            return (int)result;
        }

        public void VolumeSave() => PlayerPrefs.SetFloat(Group.name, VolumeSlider.value);
    }
}

