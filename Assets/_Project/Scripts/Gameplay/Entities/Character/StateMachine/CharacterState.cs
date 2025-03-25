using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class CharacterState : MonoBehaviour
    {
        protected CharacterStateMachine StateMachine;
        protected Character Character;

        public UnityEvent OnStateEnter;
        public UnityEvent OnStateExit;


        public virtual void Initialize(Character character, CharacterStateMachine stateMachine)
        {
            Character = character;
            StateMachine = stateMachine;
        }

        public virtual StateID GetStateID() => StateID.None;
        public virtual void StateEnter() {
            OnStateEnter?.Invoke();
        }
        public virtual void StateExit() {
            OnStateExit?.Invoke();
        }
        public virtual void StateUpdate(float deltaTime) {}
        public virtual void StateFixedUpdate(float fixedDeltaTime) {}
        public virtual void Move(Vector2 inputs) {}
        public virtual void Interact() {}
    }
}

