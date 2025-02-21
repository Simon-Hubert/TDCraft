using UnityEngine;
using UnityEngine.Events;

public class CharacterRunningState : CharacterState
{
    public UnityEvent OnEnterRunning;
    public UnityEvent OnExitRunning;
    
    public override StateID GetStateID() => StateID.Run;
    public override void StateEnter()
    {
        base.StateEnter();
        OnEnterRunning?.Invoke();
    }

    public override void StateExit()
    {
        base.StateExit();
        OnExitRunning?.Invoke();
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
        base.StateFixedUpdate(fixedDeltaTime);
        if(Character.Rb.linearVelocity.magnitude < 0.1f) StateMachine.ChangeState(StateID.Idle);
    }

    public override void BindInputs()
    {
        base.BindInputs();
        Character.PC.OnMove += ReceiveMove;
    }

    void ReceiveMove(Vector2 direction)
    {
        //MOVE PLAYER
    }
}
