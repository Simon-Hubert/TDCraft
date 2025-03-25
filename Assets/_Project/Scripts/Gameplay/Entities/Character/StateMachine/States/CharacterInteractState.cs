using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class CharacterInteractState : CharacterState
    {
        #region StateMachine Methods
        public override StateID GetStateID() => StateID.Interact;
        public override void StateEnter()
        {
            base.StateEnter();
            Debug.Log("Entering Interact State");
        }
    
        public override void StateExit()
        {
            base.StateExit();
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
    
        public override void Interact()
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
    
        public override void Move(Vector2 moves)
        {
            StateMachine.ChangeState(StateID.Run);
        }
        #endregion
    
    
    }
}

