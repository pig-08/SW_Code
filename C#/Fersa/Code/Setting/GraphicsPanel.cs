using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class GraphicsPanel : MonoBehaviour
{
    [SerializeField] private bool is16v9;
    [SerializeField] private bool hasHz;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private List<Resolution> resolutions;

    public int ResolutionIndex
    {
        get => PlayerPrefs.GetInt("ResolutionIndex", 0);
        set => PlayerPrefs.SetInt("ResolutionIndex", value);
    }

    private void Start()
    {
#if !UNITY_EDITOR
        Invoke(nameof(SetResolution), 0.1f);
#endif
    }

    private void SetResolution()
    {
        resolutions = new List<Resolution>(Screen.resolutions);
        resolutions.Reverse();

        // 전체해상도 다 가져올건지, 16:9 비율인것만 가져올건지
        if (is16v9)
        {
            resolutions = resolutions.FindAll(x => Mathf.Abs((float)x.width / x.height - (16f / 9f)) < 0.05f);
        }

        // Hz 표시여부
        if (!hasHz && resolutions.Count > 0)
        {
            List<Resolution> tempResolutions = new List<Resolution>();
            int curWidth = resolutions[0].width;
            int curHeight = resolutions[0].height;
            tempResolutions.Add(resolutions[0]);

            foreach (var resolution in resolutions)
            {
                if (curWidth != resolution.width || curHeight != resolution.height)
                {
                    tempResolutions.Add(resolution);
                    curWidth = resolution.width;
                    curHeight = resolution.height;
                }
            }
            resolutions = tempResolutions;
        }

        // 드롭다운 해상도 입력
        List<string> options = new List<string>();
        foreach (var resolution in resolutions)
        {
            string option = $"{resolution.width} x {resolution.height}";
            if (hasHz)
            {
                option += $" {resolution.refreshRate}Hz";
            }
            options.Add(option);
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = ResolutionIndex;

        resolutionDropdown.RefreshShownValue();

        DropdownOptionChanged(ResolutionIndex);
    }

    public void DropdownOptionChanged(int resolutionIndex)
    {
        ResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }
}
