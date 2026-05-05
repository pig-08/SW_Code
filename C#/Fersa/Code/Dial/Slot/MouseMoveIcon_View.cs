using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMoveIcon_View : MonoBehaviour, IView<Vector2>
{
    [SerializeField] protected RectTransform rectTransform;
    public void Init(Vector2 defaultData)
    {

    }

    public void SetData(Vector2 data) => rectTransform.anchoredPosition = data;

    public void DOTweenAllKill()
    {
        rectTransform.DOKill();
        transform.DOKill();
    }

    public void Execute(GameObject thisGameObject, PointerEventData eventData) => ExecuteEvents.Execute(thisGameObject, eventData, ExecuteEvents.endDragHandler);
    public void SetParent(Transform parent) => transform.SetParent(parent);
    public void SetParent(Transform parent, Vector2 anchorPos, float moveTime)
    {
        transform.SetParent(parent);
        rectTransform.DOAnchorPos(anchorPos, moveTime);
    }

}
