using System;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class CharacterPilotingState : CharacterState
    {

        public event Action<Vector2> OnMove;
        public event Action OnInteract;
        
        public UnityEvent OnEnterPilot;
        public UnityEvent OnExitPilot;
        #region StateMachine Methods
        public override StateID GetStateID() => StateID.Pilot;
    
        public override void StateEnter()
        {
            base.StateEnter();
            OnEnterPilot?.Invoke();
            BindInputs();
            Debug.Log("Entering Pilot State");
        }
    
        public override void StateExit()
        {
            base.StateExit();
            OnExitPilot?.Invoke();
            UnBindInputs();
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
        
        private void ReceiveMove(Vector2 dir)
        {
            OnMove?.Invoke(dir);
        }
        private void ReceiveInteract()
        {
            OnInteract?.Invoke();
            StateMachine.ChangeState(StateID.Idle);
        }
        #endregion
    }
}

