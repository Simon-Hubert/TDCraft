using UnityEngine;
using UnityEngine.Events;

public class CharacterIdleState : CharacterState
{

    public UnityEvent OnEnterIdle;
    public UnityEvent OnExitIdle;
    public override void StateEnter()
    {
        base.StateEnter();
        OnEnterIdle?.Invoke();
    }

    public override void StateExit()
    {
        base.StateExit();
        OnExitIdle?.Invoke();
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
        base.StateFixedUpdate(fixedDeltaTime);
    }
}
