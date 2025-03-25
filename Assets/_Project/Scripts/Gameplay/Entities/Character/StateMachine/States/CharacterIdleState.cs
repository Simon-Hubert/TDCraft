using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
     public class CharacterIdleState : CharacterState
     {
         #region StateMachine Methods
         public override StateID GetStateID() => StateID.Idle;
     
         public override void StateEnter()
         {
             base.StateEnter();
             Debug.Log("Entering Idle State");
         }
     
         public override void StateExit()
         {
             base.StateExit();
             Debug.Log("Exit Idle State");
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
             StateMachine.ChangeState(StateID.Interact);
         }
     
         public override void Move(Vector2 moves)
         {
             StateMachine.ChangeState(StateID.Run);
         }
         #endregion
     }   
}
