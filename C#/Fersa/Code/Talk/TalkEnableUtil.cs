using UnityEngine.SceneManagement;
using Work.PSB.Code.FieldCode.MapSaves;

namespace PSW.Code.Talk
{
    public static class TalkEnableUtil
    {
        public static bool IsEnemyDeadInScene(string enemyId)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            var state = SceneSaveSystem.LoadScene(sceneName);

            if (state == null || state.enemies == null) return false;

            int idx = state.enemies.FindIndex(e => e.id == enemyId);
            if (idx < 0) return false;

            return state.enemies[idx].isAlive == false;
        }
    }
}