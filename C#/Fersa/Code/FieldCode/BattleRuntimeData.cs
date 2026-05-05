using PSB.Code.BattleCode.Enemies;

namespace Work.PSB.Code.FieldCode
{
    public static class BattleRuntimeData
    {
        public static EnemySO[] Enemies { get; private set; }
        public static BattlePresentationSO Presentation { get; private set; }

        public static void Set(EnemySO[] enemies, BattlePresentationSO presentation)
        {
            Enemies = enemies;
            Presentation = presentation;
        }

        public static void Clear()
        {
            Enemies = null;
            Presentation = null;
        }
        
    }
}