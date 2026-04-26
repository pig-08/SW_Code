using DG.Tweening;
using Gwamegi.Code.Core.StatSystem;
using Gwamegi.Code.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpUiBa : MonoBehaviour
{
    [SerializeField] private EntityHealth _entityHealth;

    private Image _bossHpFill;

    public void Awake()
    {
        _bossHpFill = GetComponent<Image>();
    }

    public void BossDie()
    {
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }

    public void SetHpBa()
    {
        _bossHpFill.DOFillAmount(_entityHealth.GetCurrentHealth() / _entityHealth.maxHealth, _bossHpFill.fillAmount);
    }

    private void Update()
    {
        if(GameManager.Instance.isOverActive)
        {
            BossDie();
        }
    }

}
