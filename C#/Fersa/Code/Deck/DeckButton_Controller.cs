using PSW.Code.EventBus;
using System;
using UnityEngine;

public class DeckButton_Controller : MonoBehaviour
{
    [SerializeField] private DeckButton_Model model;
    [SerializeField] private DeckButton_View view;

    public Action<DeckButton_Controller> OnButtonClickEvent;

    private void OnDestroy()
    {
        OnButtonClickEvent = null;
    }

    public void InitButton(int index)
    {
        model.SetIndex(index);
        view.SetIndexText(index+1);
    }

    public void ClickButton()
    {
        OnButtonClickEvent?.Invoke(this);
        model.DeckDataList.ChangeIndex(model.GetDeckEvnet().index);
        SetImage(true);
    }

    public void SetImage(bool isUp) => view.SetImage(model.GetPopData(isUp));
}

public struct DeckEvnet : IEvent
{
    public int index;
}

