using System.Collections.Generic;
using Gameplay;
using Inputs;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour, IControllable
    {
        #region Comps
        [Header("Components")]
        public PlayerController PC;
        public InteractionsCollider ICollider;
        #endregion
    
        #region StateMachine
        [Header("StateMachine")]
        [SerializeField] private List<CharacterState> _states;

        private CharacterStateMachine _stateMachine;
        #endregion
    
        #region Variables
        private bool _isMoving = false;
        #endregion
        
        private void Awake()
        {
            PC.Bind(this);
            _stateMachine = new CharacterStateMachine();
            foreach (CharacterState state in GetComponents<CharacterState>())
            {
                _states.Add(state);
                state.Initialize(this, _stateMachine);
            }
        }

        private void Start()
        {
            _stateMachine.Initialize(_states);
            _stateMachine.Start(_stateMachine.GetState(StateID.Idle));
        }

        private void Update()
        {
            _stateMachine.CurrentState.StateUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.StateFixedUpdate(Time.fixedDeltaTime);
        }


        public void ReceiveMove(Vector2 inputs) {
            _stateMachine.CurrentState.Move(inputs);
        }
        public void ReceiveInteract() {
            _stateMachine.CurrentState.Interact();
        }
    }
}