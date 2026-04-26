using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
    [field: SerializeField] private PlayerStateSO[] playerStateSOList;

    private Dictionary<Type,IPlayerComponent> _componentList = new Dictionary<Type, IPlayerComponent>();
    private PlayerStateMachine _stateMachine;

    private bool _isSetting;
    private bool _isGameOver;
    private bool _isGameEnd;

    private void Awake()
    {
        PlayerInput.SetPlayerInput(true);
        AddComponentList();
        ComponentListInit();
        _stateMachine = new PlayerStateMachine(this,playerStateSOList);
        ChangeState("IDLE");
    }
    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdateStateMachine();
    }

    private void AddComponentList()
    {
        GetComponentsInChildren<IPlayerComponent>().ToList().ForEach((v) => _componentList.Add(v.GetType(),v));
    }
    private void ComponentListInit()
    {
        _componentList.Values.ToList().ForEach(v => v.Init(this));
    }
    public T GetCompo<T>() where T : IPlayerComponent
    {
        return (T)_componentList.GetValueOrDefault(typeof(T));
    }

    public void ChangeState(string newStateName, bool forced = false) => _stateMachine.ChangeState(newStateName, forced);

    public void SetSetting(bool isSetting) => _isSetting = isSetting;
    public bool GetSetting() => _isSetting;
    public void GameEnd() => _isGameEnd = true;
    public bool GetGameEnd() => _isGameEnd;
    public void SetGameOver(bool isGameOver) => _isGameOver = isGameOver;
    public bool GetGameOver() => _isGameOver;
}

