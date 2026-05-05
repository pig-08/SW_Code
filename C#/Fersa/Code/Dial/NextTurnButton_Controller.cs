using UnityEngine;
using Work.CSH.Scripts.Managers;

public class NextTurnButton_Controller : MonoBehaviour
{
    [SerializeField] private TurnManagerSO turnManagerSO;

    public void NextTurn()
    {
        if (turnManagerSO.Turn)
            turnManagerSO.NextTurn();
    }
}
