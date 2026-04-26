using UnityEngine;

namespace SW.Code.SO
{
    [CreateAssetMenu(fileName = "TestTurretSO", menuName = "SO/TurretData/TestTurretSO")]
    public class TestTurretSO : ScriptableObject
    {
        [Header("터렛 이미지")]
        public Sprite TurretImage;

        [Header("이름")]
        public string TurretName;

        [Header("공격")]
        public float FireSpeed;
        public float FireDamage;

        [Header("가격")]
        public int Price;

        [Header("등급")]
        public TestTurretRating TurretRating;

        [Header("특수")]
        public bool IsSpecial;
        public int SpecialPrice;
        public int id = 0;

        //OnValidate 같은 기능인데 이거 안하면 빌드때 오류남
        private void OnEnable()
        {
            if ((int)TurretRating >= 0 && (int)TurretRating <= 2)
                IsSpecial = true;
            else
                IsSpecial = false;
        }

        private void OnValidate()
        {
            if ((int)TurretRating >= 0 && (int)TurretRating <= 2)
                IsSpecial = true;
            else
                IsSpecial = false;
        }
    }

    public enum TestTurretRating
    {
        SSS = 1, SS, S,
        A, B, C, D, F
    }
}