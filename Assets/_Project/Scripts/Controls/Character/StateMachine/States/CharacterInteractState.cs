using System.Data;
using Controls;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterInteractState : CharacterState
    {
        #region UnityEvent
        public UnityEvent OnEnterInteract;
        public UnityEvent OnExitInteract;
        #endregion
        
        
        #region StateMachine Methods
        public override StateID GetStateID() => StateID.Interact;
        public override void StateEnter()
        {
            base.StateEnter();
            OnEnterInteract?.Invoke();
            BindInputs();
            Debug.Log("Entering Interact State");
        }
    
        public override void StateExit()
        {
            base.StateExit();
            OnExitInteract?.Invoke();
            UnBindInputs();
            Debug.Log("Exiting Interact State");
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
            if (Character.ICollider.Interactables.Count > 0)
            {
                if (Character.ICollider.Interactables[0].GetInteractableID() == InteractableID.Module &&
                    Character.ICollider.Interactables[0].Interact(Character))
                {
                    StateMachine.ChangeState(StateID.Pilot);
                }

            }
        }
    
        void ReceiveMove(Vector2 moves)
        {
            StateMachine.ChangeState(StateID.Run);
        }
        #endregion
    
    
    }
}

