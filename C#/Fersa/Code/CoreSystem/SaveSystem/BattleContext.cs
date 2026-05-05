namespace PSB.Code.CoreSystem.SaveSystem
{
    public static class BattleContext
    {
        public static bool HasContext { get; private set; }
        public static string FieldSceneName { get; private set; }
        public static string FieldEnemyId { get; private set; }
        public static bool ExitBySetting { get; private set; }

        public static void Set(string fieldSceneName, string fieldEnemyId)
        {
            HasContext = true;
            FieldSceneName = fieldSceneName;
            FieldEnemyId = fieldEnemyId;
            ExitBySetting = false;
        }

        public static void Clear()
        {
            HasContext = false;
            FieldSceneName = null;
            FieldEnemyId = null;
            ExitBySetting = false;
        }
        
    }
}