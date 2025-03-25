using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class PlayerController : MonoBehaviour
    {
        private IControllable controlled;

        public void Move(InputAction.CallbackContext context) {
            controlled.ReceiveMove(context.ReadValue<Vector2>());
        }

        public void Interact(InputAction.CallbackContext context) {
            controlled.ReceiveInteract();
        }

        public void Bind(IControllable controllable) {
            controlled = controllable;
        }
    }
}