using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private List<IUi> uiList;

    private void Awake()
    {
        uiList = new List<IUi>();
        GetComponentsInChildren(uiList);
        foreach (IUi ui in uiList) ui.Init();
    }
}
