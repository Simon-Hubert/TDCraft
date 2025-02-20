using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterIdleState _idleState;
    CharacterStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new CharacterStateMachine();

        _idleState.Initialize(this, _stateMachine);
    }

    private void Start()
    {
        _stateMachine.Initialize(_idleState);
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
