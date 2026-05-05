using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSW.Code.BaseSystem
{
    public class BaseSystemUIManager : MonoBehaviour
    {
        protected List<IBaseSystemUI> baseSystemUIList;

        private void Awake()
        {
            baseSystemUIList = GetComponentsInChildren<IBaseSystemUI>().ToList();
        }

        public void AllBaseInit()
        {
            baseSystemUIList.ForEach(v => v.DataInit());
        }

        public void AllBaseDestroy()
        {
            baseSystemUIList.ForEach(v => v.DataDestroy());
        }
    }
}