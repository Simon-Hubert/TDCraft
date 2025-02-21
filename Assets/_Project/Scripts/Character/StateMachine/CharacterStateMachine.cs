using UnityEngine;

public enum StateID
{
    Idle,
    Run,
    Interact,
    None
};
public class CharacterStateMachine
{
    public CharacterState CurrentState;
    public CharacterState[] States;

    public void Initialize(CharacterState initialState, CharacterState[] states)
    {
        CurrentState = initialState;
        States = states;
        CurrentState.StateEnter();
    }

    public void ChangeState(StateID newStateID)
    {
        CurrentState.StateExit();
        CurrentState = GetState(newStateID);
        if(CurrentState != null) CurrentState.StateEnter();
    }

    public CharacterState GetState(StateID newStateID)
    {
        for (int i = 0; i < States.Length - 1; i++)
        {
            if(States[i].GetStateID() == newStateID) return States[i];
        }

        return null;
    }
    
}
