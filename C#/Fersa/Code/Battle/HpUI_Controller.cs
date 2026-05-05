using UnityEngine;

namespace PSW.Code.Battle
{
    public class HpUI_Controller : MonoBehaviour
    {
        [SerializeField] private HpUI_View view;
        [SerializeField] private HpUI_Model model;
        public void Init(float currentHp, float maxHp, bool isLeft = true)
        {
            model.ChangeCurrentHp(currentHp);
            model.ChangeMaxHp(maxHp);
            //view.SetTextPos(model.GetTextPosData(isLeft));
            view.Init(model.InitModel());
        }
        public void ChangeCurrentHp(float currentHp)
        {
            model.ChangeCurrentHp(currentHp);
            view.SetData(model.GetModelData());
        }
        public void ChangeMaxHp(float maxHp)
        {
            model.ChangeMaxHp(maxHp);
            view.SetData(model.GetModelData());
        }
    }
}