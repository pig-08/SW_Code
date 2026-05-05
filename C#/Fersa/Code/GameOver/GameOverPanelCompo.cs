using UnityEngine;

namespace PSW.Code.GameOver
{
    public abstract class GameOverPanelCompo<T1, T2> : MonoBehaviour
    {
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected Transform content;

        public abstract T2 NewSlot(T1 slotData);
    }
}