using PSB.Code.BattleCode.Enemies;
using PSB.Code.BattleCode.Enums;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Work.PSB.Code.FieldCode;

public class BossBanner_Controller : MonoBehaviour
{
    [SerializeField] private BossBanner_Model model;
    [SerializeField] private BossBanner_View view;

    private async void Start()
    {
        await Awaitable.NextFrameAsync();

        EnemySO[] enemies = BattleRuntimeData.Enemies;
        if (enemies == null || enemies.Length == 0)
            return;

        foreach (EnemySO enemy in enemies)
        {
            if (enemy == null) continue;

            if (enemy.grade == EnemyGrade.Boss)
            {
                SetUpBoss(enemy);
                break;
            }
        }
    }

    public async void SetUpBoss(EnemySO bossData)
    {
        print(bossData.enemyName);
        view.SetBossData(bossData.icon, bossData.enemyName);
        view.PlayBannerCilp(model.GetCilpName(true));
        await Awaitable.WaitForSecondsAsync(model.GetDelayTime());
        view.PlayBannerCilp(model.GetCilpName(false));
    }
}
