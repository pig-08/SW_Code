using System.Collections;
using PSB_Lib.Dependencies;
using PSB.Code.BattleCode.Players;
using UnityEngine;
using Work.CSH.Scripts.Battle;
using PSW.Code.Dial;

namespace Work.PSB.Code.CoreSystem
{
    public class BattleTurnCoordinator : MonoBehaviour
    {
        [SerializeField] private BattleEnterContextSO enterContext;
        [SerializeField] private TurnBeforeExecutor turnBeforeExecutor;
        [SerializeField] private SkillPanels_Controller skillPanelsController;

        [Inject] private PlayerManager _playerManager;

        private void Start()
        {
            StartCoroutine(Co_StartTurnWhenReady());
        }

        private IEnumerator Co_StartTurnWhenReady()
        {
            if (enterContext == null)
            {
                Debug.LogError("BattleTurnCoordinator: enterContext 미할당");
                yield break;
            }

            if (!enterContext.TryConsume(out var by))
                yield break;

            while (_playerManager == null || _playerManager.BattlePlayer == null || _playerManager.BattlePlayer.TurnManager == null)
                yield return null;

            var tm = _playerManager.BattlePlayer.TurnManager;

            yield return new WaitForSeconds(0.6f);

            yield return new WaitUntil(() => turnBeforeExecutor.Execute(by));
            yield return new WaitForSeconds(0.5f);


            
            if (UnityEngine.Random.value > 0.5f)
            {
                tm.SetPlayerTurn();

            }
            else
            {
                tm.SetEnemyTurn();

            }
        }
        
    }
}