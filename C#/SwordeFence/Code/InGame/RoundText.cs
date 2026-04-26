using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Managers;
using SW.Code.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundText : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO shopOnOffEventChannel;

    private TextMeshProUGUI _roundText;
    private Spawn _spawn;
    private void Start()
    {
        _roundText = GetComponent<TextMeshProUGUI>();

        _spawn = Manager.GetCompoManager<Spawn>();
        shopOnOffEventChannel.AddListener<StopOnOffEvent>(SetWaveText);
    }

    private void OnDisable()
    {
        shopOnOffEventChannel.RemoveListener<StopOnOffEvent>(SetWaveText);
    }

    private void SetWaveText(StopOnOffEvent evt)
    {
        _roundText.text = "Round:" + _spawn.GetBossWave().ToString();
    }

}
