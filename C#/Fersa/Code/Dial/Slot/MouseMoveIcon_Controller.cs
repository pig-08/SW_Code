using PSW.Code.Dial;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MouseMoveIcon_Controller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private MouseMoveIcon_View view;
    [SerializeField] private MouseMoveIcon_Model model;

    public virtual void SetDonMoveIcon(bool isDonMove) => model.SetDonMoveIcon(isDonMove);
    public virtual void MovePosParentTrm() => view.SetParent(model.GetParentTrm(), Vector2.zero, model.GetNotAddSkillIconMoveTime());

    private void Update()
    {
        if (model.GetIsDonMove() && model.GetIsDrag())
        {
            view.Execute(gameObject, model.GetEventData());
            model.SetIsDrag(false);
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (model.GetIsDonMove()) return;

        view.DOTweenAllKill();
        model.SetIsDrag(true);

        //OnDragEvent?.Invoke();
        view.SetParent(model.GetMoveIconCanvas().transform);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (model.GetIsDonMove()) return;

        view.SetData(model.GetMousePos(eventData));
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        (Transform, MonoBehaviour) temp = model.GetParentTrmAndChoiceSkillSlot();
        view.DOTweenAllKill();

        model.SetIsDrag(false);
        view.SetParent(temp.Item1, Vector2.zero, model.GetNotAddSkillIconMoveTime());
    }
}
