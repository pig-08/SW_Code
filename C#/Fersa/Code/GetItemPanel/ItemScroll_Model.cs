using PSB.Code.BattleCode.Items;
using UnityEngine;

public class ItemScroll_Model : ModelCompo<float>
{
    [SerializeField] private GameObject ItemPanelView;
    [SerializeField] private float panelOpenTime = 5f;

    public override float InitModel()
    {
        return panelOpenTime;
    }

    
}
