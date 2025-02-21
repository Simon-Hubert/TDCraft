using System;
using Controls;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Comps
    [Header("Components")]
    public PlayerController PC;

    public Rigidbody2D Rb;
    #endregion
    #region StateMachine
    [Header("StateMachine")]
    [SerializeField] CharacterIdleState _idleState;
    [SerializeField] CharacterRunningState _runningState;

    private CharacterStateMachine _stateMachine;
    #endregion

    private bool _isMoving = false;

    private void Awake()
    {
        _stateMachine = new CharacterStateMachine();

        _idleState.Initialize(this, _stateMachine);
        _runningState.Initialize(this, _stateMachine);
    }

    private void Start()
    {
        _stateMachine.Initialize(_idleState, GetComponents<CharacterState>());
    }

    private void Update()
    {
        _stateMachine.CurrentState.StateUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.StateFixedUpdate(Time.fixedDeltaTime);
    }
    
}
