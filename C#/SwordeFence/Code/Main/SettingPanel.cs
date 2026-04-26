using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace SW.Code.Main
{
    public class SettingPanel : MainComponent
    {
        [SerializeField] private AudioMixer mixer;

        private Slider[] musicSliderList = new Slider[2];
        private string[] musucNameList = { "BGM", "VFX" };

        public override void Init(VisualElement root)
        {
            base.Init(root);

            for (int i = 0; i < musicSliderList.Length; i++)
            {
                int num = i;
                musicSliderList[num] = _panel.Q<Slider>(musucNameList[num] + "Slider");
                if (mixer.GetFloat(musucNameList[num], out float value))
                {
                    musicSliderList[num].value = value;
                    musicSliderList[num].RegisterValueChangedCallback(evt => SetMusicVolume(evt.newValue, musucNameList[num]));
                }
            }
        }

        private void SetMusicVolume(float sliderValue, string musucName) => mixer.SetFloat(musucName, sliderValue);
    }
}