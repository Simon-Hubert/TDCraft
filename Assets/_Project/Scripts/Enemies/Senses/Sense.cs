using System;
using UnityEngine;

namespace Enemies
{
    public abstract class Sense : MonoBehaviour
    {
        public event Action<Vector2> OnSenseTarget;

        protected void SensedAt(Vector2 position) {
            OnSenseTarget?.Invoke(position);
        }
    }
}
