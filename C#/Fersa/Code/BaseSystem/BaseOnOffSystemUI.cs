using DG.Tweening;
using Newtonsoft.Json.Bson;
using System;
using UnityEngine;

namespace PSW.Code.BaseSystem
{
    public class BaseOnOffSystemUI : MonoBehaviour
    {
        [SerializeField] protected int weight;
        [SerializeField] private UIType thisType;
        [SerializeField] protected BaseSystemUIManager manager;

        public Func<int, BaseOnOffSystemUI, bool> OnPopUpEvent;
        public UIType GetThisType() => thisType;
        public bool IsOnPopUp() => OnPopUpEvent.Invoke(weight, this);
        public bool IsHighWeight(int weight)
        {
            return weight > this.weight;
        }

        public virtual void PopUp()
        {
            manager?.AllBaseInit();
        }
        public virtual void PopDown()
        {
            manager?.AllBaseDestroy();
        }
    }
}