using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingPanenlManager : MonoBehaviour
{
    private List<SettingCompo> _settingCompoList;
    private void Awake()
    {
        _settingCompoList = new List<SettingCompo>();
        _settingCompoList = GetComponentsInChildren<SettingCompo>().ToList();
        _settingCompoList.ForEach(v => v.Init());
    }

    public void SetPanel(SettingCompo settingCompo)
    {
        SetAllPanelClose();

        settingCompo.Open();
    }

    public void SetAllPanelClose()
    {
        foreach (SettingCompo setting in _settingCompoList)
            setting.Close();
    }
}
