using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Character
{
    public class CharacterState : MonoBehaviour
    {
        protected CharacterStateMachine StateMachine;
        protected Character Character;


        public virtual void Initialize(Character character, CharacterStateMachine stateMachine)
        {
            Character = character;
            StateMachine = stateMachine;
        }

        public virtual StateID GetStateID() => StateID.None; 
        public virtual void StateEnter() {}
        public virtual void StateExit() {}
        public virtual void StateUpdate(float deltaTime) {}
        public virtual void StateFixedUpdate(float fixedDeltaTime) {}
        public virtual void BindInputs() {}
        public virtual void UnBindInputs() {}
    
    }
}

