using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; set; }
    private Dictionary<string, PlayerState> _states;

    public PlayerStateMachine(Player player, PlayerStateSO[] stateList)
    {
        _states = new Dictionary<string, PlayerState>();
        foreach (PlayerStateSO state in stateList)
        {
            Type type = Type.GetType(state.className);
            Debug.Assert(type != null, $"Finding type is null : {state.className}");
            PlayerState entityState = Activator.CreateInstance(type, player, state.animayionHash) as PlayerState;
            _states.Add(state.stateName, entityState);
        }

    }

    public void ChangeState(string newStateName, bool forced = false)
    {
        PlayerState newState = _states.GetValueOrDefault(newStateName);  
        Debug.Assert(newState != null, $"Finding state is null : {newStateName}");

        if (forced = false && CurrentState == newState)
            return; 

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void UpdateStateMachine()
    {
        CurrentState?.Update();
    }

    public void FixedUpdateStateMachine()
    {
        CurrentState?.FixedUpdate();
    }
}
