using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListPanel : SettingCompo
{
    [SerializeField] private List<EnemyDataSO> enemyDataSOList;
    [SerializeField] private GameObject enemyCardPrefab;

    [SerializeField] private Transform content;
    public override void Init()
    {
        base.Init();

        foreach(EnemyDataSO data in enemyDataSOList)
        {
            EnemyCard card = Instantiate(enemyCardPrefab, content).GetComponent<EnemyCard>();
            card.Init(data);
        }
    }

    public override void Open()
    {
        content.DOMoveY(0, 0);
        base.Open();
    }
}
