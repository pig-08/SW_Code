using SW.Code.Stat;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.SO
{
    [CreateAssetMenu(fileName = "StatSOList",menuName = "SO/StatSyatem/StatSOListSO")]
    public class StatSOListSO : ScriptableObject
    {
        public List<StatSO> statSOListSO;
    }
}