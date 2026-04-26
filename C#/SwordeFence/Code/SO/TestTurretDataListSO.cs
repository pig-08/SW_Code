using SW.Code.SO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Test
{
    [CreateAssetMenu(fileName = "TurretDataList", menuName = "SO/TurretData/TurretDataList")]
    public class TestTurretDataListSO : ScriptableObject
    {
        public List<TestTurretSO> testTurretDataList;

        private void OnEnable()
        {
            try
            {
                for (int i = 1; i <= testTurretDataList.Count; i++)
                {
                    int setId = i;

                    if (testTurretDataList[i - 1].id == 0)
                        testTurretDataList[i - 1].id = setId;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.Log($"{ex}로 오류남 처리");
            }
        }

        private void OnValidate()
        {
            try
            {
                for (int i = 1; i <= testTurretDataList.Count; i++)
                {
                    int setId = i;

                    if (testTurretDataList[i - 1].id == 0)
                        testTurretDataList[i - 1].id = setId;
                }
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Debug.Log($"{ex}로 오류남 처리");
            }


        }
    }
}