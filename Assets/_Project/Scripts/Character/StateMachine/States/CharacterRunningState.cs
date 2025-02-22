using UnityEngine;
using UnityEngine.Events;

public class CharacterRunningState : CharacterState
{
    [SerializeField] private float _speed;
    public UnityEvent OnEnterRunning;
    public UnityEvent OnExitRunning;
    
    bool isRunning = false;
    private Vector2 dir;
    
    
    public override StateID GetStateID() => StateID.Run;
    public override void StateEnter()
    {
        base.StateEnter();
        OnEnterRunning?.Invoke();
        BindInputs();
        Debug.Log("Entering Run State");
    }

    public override void StateExit()
    {
        base.StateExit();
        OnExitRunning?.Invoke();
        UnBindInputs();
        Debug.Log("Exiting Run State");
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    public override void StateFixedUpdate(float fixedDeltaTime)
    {
        base.StateFixedUpdate(fixedDeltaTime);
        if(!isRunning) StateMachine.ChangeState(StateID.Idle);
        else
        {
            Character.Rb.AddForce(dir * _speed);
            Debug.Log(dir);
        }

    }

    public override void BindInputs()
    {
        base.BindInputs();
        Character.PC.OnMove += ReceiveMove;
    }

    public override void UnBindInputs()
    {
        base.UnBindInputs();
        Character.PC.OnMove -= ReceiveMove;
    }

    void ReceiveMove(Vector2 direction)
    {
        if ((direction.x >= 0.1f || direction.y >= 0.1f) || (direction.x <= -0.1f || direction.y <= -0.1f))
        {
            isRunning = true;
            dir = direction;
        }
        else
        {
            isRunning = false;
            dir = Vector2.zero;
        }
    }
}
