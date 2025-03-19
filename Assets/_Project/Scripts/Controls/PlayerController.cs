using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<Vector2> OnMove;
        public event Action OnInteract;
        
        public void Move(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        public void Interact(InputAction.CallbackContext context)
        {
            //Call sur le premier Interactable de la list
            OnInteract?.Invoke();
        }
    } 
}

