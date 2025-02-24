using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public enum StateID
    {
        Idle,
        Run,
        Interact,
        Pilot,
        None
    };
    public class CharacterStateMachine
    {
        public CharacterState CurrentState;
        public List<CharacterState> States = new List<CharacterState>();

        public void Initialize(List<CharacterState> states)
        {
            States = states;
        }

        public void Start(CharacterState initialState)
        {
            CurrentState = initialState;
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
            for (int i = 0; i < States.Count; i++)
            {
                if (States[i].GetStateID() == newStateID) return States[i];
            }

            return null;
        }
    
    }
}

