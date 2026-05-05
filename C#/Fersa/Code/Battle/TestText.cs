using UnityEngine;

namespace PSW.Code.Battle
{
    public class TestText : MonoBehaviour
    {
        [SerializeField] private HpUI_Controller test;

        private void Awake()
        {
            HpData a = new HpData();
            a.maxHp = 100;
            a.currentHp = 50;
            a.time = 0.2f;
            test.Init(50, 100);
        }

        private void Update()
        {

        }
    }
} 