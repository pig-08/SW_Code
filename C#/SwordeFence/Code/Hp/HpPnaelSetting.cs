using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Core.Managers;
using Gwamegi.Code.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPnaelSetting : MonoBehaviour
{
    [SerializeField] private MapHealth mapHealth;
    [SerializeField] private GameEventChannelSO mapEventChannel;
    [SerializeField] private GameObject hpPrefab;
    private List<Animator> hpAnimatorList = new List<Animator>();

    private void Start()
    {
        mapEventChannel.AddListener<HealthChangeEvent>(SetHpPanel);
        StartCoroutine(InitTime());
    }

    private void OnDestroy()
    {
        mapEventChannel.RemoveListener<HealthChangeEvent>(SetHpPanel);
        
    }

    private IEnumerator InitTime()
    {
        yield return null;
        for (int i = 0; i < mapHealth.GetCurrentHp(); i++)
            hpAnimatorList.Add(Instantiate(hpPrefab, transform).GetComponent<Animator>());
    }

    public void SetHpPanel(HealthChangeEvent evt) => StartCoroutine(SettingTime(evt));

    private IEnumerator SettingTime(HealthChangeEvent evt)
    {
        yield return null;
        
        for(int i = mapHealth.GetCurrentHp() + evt.value - 1; i >= mapHealth.GetCurrentHp(); i--)
            hpAnimatorList[i].Play("HpDelete");

    }
}
