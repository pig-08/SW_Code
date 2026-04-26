using SW.Code.SO;
using SW.Code.Test;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SW.Code.Temp
{
    public class TempShopPanel : MonoBehaviour
    {
        [SerializeField] private TestTurretSO[] turretDataList;
        [SerializeField] private TempButton[] turretButtonList;

        public void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                turretButtonList[i].Init(turretDataList[i]);
            }
        }


    }
}