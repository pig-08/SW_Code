using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Dial.Slot
{
    public class SkillIconSetting_Model : ModelCompo<SizeData>
    {
        [Header("PopUp(Parents)")]
        [SerializeField] protected SizeData sizeData;
        [SerializeField] protected SizeData rotationData;
        [SerializeField] protected float popTime = 0.3f;

        protected bool _isPopUp;
        protected bool _isPopMove;

        public float GetPopTime() => popTime;
        public bool GetPopUp() => _isPopUp;
        public bool GetPopMove() => _isPopMove;
        public virtual Vector2 GetSize() => sizeData.GetSize(_isPopUp);
        public virtual Vector2 GetSize(bool isPopUp) => sizeData.GetSize(isPopUp);
        public virtual Vector3 GetRotation() => rotationData.GetSize(_isPopUp);
        public override SizeData InitModel() => sizeData;
        public void SetPopUp() => _isPopUp = !_isPopUp;
        public void SetPopUp(bool isPopUp) => _isPopUp = isPopUp;
        public virtual void SetPopMove(bool popMove) => _isPopMove = popMove;
    }
}