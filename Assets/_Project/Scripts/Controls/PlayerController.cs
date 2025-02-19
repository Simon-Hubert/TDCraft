using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields
        [Header("Variables")]
        [SerializeField]float _moveSpeed = 5.0f;
        
        [Space(20.0f)]
        
        [Header("Components")]
        [SerializeField] InteractionsCollider _interactionsCollider;
        [SerializeField] private Rigidbody2D _rb;
        #endregion
        
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();
            if (move != Vector2.zero)
            {
                _rb.AddForce(_moveSpeed * move);
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
            }
        }

        public void Interact(InputAction.CallbackContext context)
        {
            //Call sur le premier Interactable de la list
        }
    } 
}

