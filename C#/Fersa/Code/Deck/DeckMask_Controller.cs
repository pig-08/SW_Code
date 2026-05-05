using PSW.Code.BaseSystem;
using PSW.Code.Input;
using UnityEngine;

public class DeckMask_Controller : MonoBehaviour, IBaseSystemUI
{
    [SerializeField] private DeckMask_Model model;
    [SerializeField] private DeckMask_View view;

    private void Awake()
    {
        model.InitModel();
    }
    public void DataReSet()
    {
        DeckDataSaver.ReSetData();
    }

    public void DataInit()
    {
        model.SetIsUp(true);
    }

    public void DataDestroy()
    {
        model.SetIsUp(false);
    }
}
