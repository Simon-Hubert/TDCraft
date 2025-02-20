using UnityEngine;

public class CharacterStateMachine
{
    public CharacterState CurrentState;

    public void Initialize(CharacterState initialState)
    {
        CurrentState = initialState;
        CurrentState.StateEnter();
    }

    public void ChangeState(CharacterState newState)
    {
        CurrentState.StateExit();
        CurrentState = newState;
        CurrentState.StateEnter();
    }
}
