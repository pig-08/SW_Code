using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Input;
using Gwamegi.Code.Managers;
using UnityEngine;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] private MapHealth mapHealth;
    [SerializeField] private GameEventChannelSO mapHealthEventChannel;
    [SerializeField] private RectTransform messageBox;
    [SerializeField] private GameInputSO gameInputSO;

    public void GameOver()
    {
        messageBox.gameObject.SetActive(false);
        gameObject.SetActive(false);
        HealthChangeEvent evt = new HealthChangeEvent();
        evt.value = mapHealth.GetCurrentHp();
        mapHealthEventChannel.RaiseEvent(evt);
    }
    public void OnMouseEnterEvent()
    {
        messageBox.gameObject.SetActive(true);
        messageBox.anchoredPosition = gameInputSO.mousePosition;
    }

    public void OnMouseExitEvent()
    {
        messageBox.gameObject.SetActive(false);
    }
}
