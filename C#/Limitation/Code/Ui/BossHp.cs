using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    [SerializeField] private Image bossHpBa;
    [SerializeField] private Transform hpUiTrm;

    private float max = 100;
    private float curr = 100;

    private void Awake()
    {
        hpUiTrm.DOLocalMoveY(1000, 0);
    }

    public void OnHpBa()
    {
        hpUiTrm.DOLocalMoveY(410, 0.5f);
    }

    private void SetHpBa(float hp, float maxHp)
    {
        bossHpBa.DOFillAmount(hp/maxHp,0.2f);
    }
}
