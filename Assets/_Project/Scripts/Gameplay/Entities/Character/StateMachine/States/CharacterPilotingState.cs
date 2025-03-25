using System;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class CharacterPilotingState : CharacterState
    {
        public event Action<Vector2> OnMove;
        public event Action OnInteract;
        
        #region StateMachine Methods
        public override StateID GetStateID() => StateID.Pilot;
    
        public override void StateEnter()
        {
            base.StateEnter();
            Debug.Log("Entering Pilot State");
        }
    
        public override void StateExit()
        {
            base.StateExit();
            Debug.Log("Exit Pilot State");
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
        public override void Move(Vector2 dir)
        {
            OnMove?.Invoke(dir);
        }
        public override void Interact()
        {
            OnInteract?.Invoke();
            StateMachine.ChangeState(StateID.Idle);
        }
        #endregion
    }
}

