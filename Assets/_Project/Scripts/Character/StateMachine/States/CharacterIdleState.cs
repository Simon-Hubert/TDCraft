using UnityEngine;
using UnityEngine.Events;

public class CharacterIdleState : CharacterState
{
    #region UnityEvents
    public UnityEvent OnEnterIdle;
    public UnityEvent OnExitIdle;
    #endregion

    
    #region StateMachine Methods
    public override StateID GetStateID() => StateID.Idle;

    public override void StateEnter()
    {
        base.StateEnter();
        OnEnterIdle?.Invoke();
        BindInputs();
        Debug.Log("Entering Idle State");
    }

    public override void StateExit()
    {
        base.StateExit();
        OnExitIdle?.Invoke();
        UnBindInputs();
        Debug.Log("Exit Idle State");
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
        base.StateFixedUpdate(fixedDeltaTime);
    }
    #endregion

    #region Inputs Methods
    public override void BindInputs()
    {
        base.BindInputs();
        Character.PC.OnInteract += ReceiveInteract;
        Character.PC.OnMove += ReceiveMove;
    }

    public override void UnBindInputs()
    {
        base.UnBindInputs();
        Character.PC.OnInteract -= ReceiveInteract;
        Character.PC.OnMove -= ReceiveMove;
    }

    void ReceiveInteract()
    {
        StateMachine.ChangeState(StateID.Interact);
    }

    void ReceiveMove(Vector2 moves)
    {
        StateMachine.ChangeState(StateID.Run);
    }
    #endregion
}
