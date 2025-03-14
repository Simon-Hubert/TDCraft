using System;
using UnityEngine;

namespace Enemies
{
    public abstract class Sense : MonoBehaviour
    {
        public event Action<Transform> OnSenseTarget;

        protected void SensedAt(Transform target) {
            OnSenseTarget?.Invoke(target);
        }
    }
}
