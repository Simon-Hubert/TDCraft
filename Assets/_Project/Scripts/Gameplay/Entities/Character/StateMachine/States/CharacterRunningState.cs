using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class CharacterRunningState : CharacterState
    {
        #region Fields
        [SerializeField] private float _speed;
        #endregion
        
        #region Variables
        bool isRunning = false;
        private Vector2 dir;
        #endregion
    
    
        #region StateMachine Methods
        public override StateID GetStateID() => StateID.Run;
        public override void StateEnter()
        {
            base.StateEnter();
            Debug.Log("Entering Run State");
        }
    
        public override void StateExit()
        {
            base.StateExit();
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
                Character.transform.position += new Vector3 (dir.x * (_speed * fixedDeltaTime), dir.y * (_speed * fixedDeltaTime) );
                Debug.Log(dir);
            }
    
        }
        #endregion
    
        #region Inputs Methods
        public override void Move(Vector2 direction)
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
        #endregion
    }
}