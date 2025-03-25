using UnityEngine;

namespace Inputs
{
    public interface IControllable
    {
        void ReceiveMove(Vector2 inputs);
        void ReceiveInteract();
    }
}
