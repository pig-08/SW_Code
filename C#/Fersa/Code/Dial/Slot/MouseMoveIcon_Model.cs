using PSW.Code.Dial;
using UnityEngine;
using UnityEngine.EventSystems;
using Work.CSH.Scripts.Managers;

public class MouseMoveIcon_Model : ModelCompo<Vector2>
{
    [SerializeField] private TurnManagerSO turnManager;

    [SerializeField] private LayerMask currentSkillLayer;
    [SerializeField] private float notAddSkillIconMoveTime = 0.3f;
    [SerializeField] private int installRange = 60;

    private Canvas _moveIconCanvas;
    private Transform _parentTrm;
    private PointerEventData _eventData;
    private Collider2D[] _currentSlotColliders;

    private Vector2 _mousePos;

    private bool _donMoveIcon;
    private bool _isDrag;
    public override Vector2 InitModel()
    {
        return _currentData;
    }
    #region Get
    public Transform GetParentTrm() => _parentTrm;
    public Canvas GetMoveIconCanvas() => _moveIconCanvas;
    public PointerEventData GetEventData() => _eventData;
    public Vector2 GetMousePos(PointerEventData eventData = null)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        transform.parent as RectTransform, eventData.position,
        null, out _mousePos);

        return _mousePos;
    }
    public (Transform, ChoiceSkillSlot) GetParentTrmAndChoiceSkillSlot()
    {
        _currentSlotColliders = Physics2D.OverlapCircleAll(transform.position, installRange, currentSkillLayer);

        if (_currentSlotColliders.Length <= 0)
            return (_parentTrm, null);
        else
        {
            int minSizeIndex = 0;
            float minSize = (_currentSlotColliders[0].transform.position - transform.position).sqrMagnitude;

            for (int i = 1; i < _currentSlotColliders.Length; ++i)
            {
                if (minSize > (_currentSlotColliders[i].transform.position - transform.position).sqrMagnitude)
                {
                    minSizeIndex = i;
                    minSize = (_currentSlotColliders[i].transform.position - transform.position).sqrMagnitude;
                }
            }

            ChoiceSkillSlot choiceSkill = _currentSlotColliders[minSizeIndex].GetComponent<ChoiceSkillSlot>();

            return (_currentSlotColliders[minSizeIndex].transform, choiceSkill);
        }
    }
    public float GetNotAddSkillIconMoveTime() => notAddSkillIconMoveTime;
    public bool GetIsDonMove() => turnManager.Turn == false || _donMoveIcon;
    public bool GetIsDrag() => _isDrag;
    #endregion

    #region Set
    public void SetIsDrag(bool isDrag) => _isDrag = isDrag; 
    public void SetDonMoveIcon(bool isDonMoveIcon) => _donMoveIcon = isDonMoveIcon;
    #endregion
}
