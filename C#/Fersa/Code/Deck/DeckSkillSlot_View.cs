using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DeckSkillSlot_View : MonoBehaviour, IView<int>
{
    [SerializeField] private Image lockOn;

    public void Init(int defaultData){}
    public void SetData(int data) {}
    public void SetLockOn(bool isLock, float time) => lockOn.DOFade(isLock ? 1 : 0, time);
}
