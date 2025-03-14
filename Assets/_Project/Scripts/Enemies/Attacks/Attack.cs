using System;
using UnityEngine;

namespace Enemies
{
    public abstract class Attack : MonoBehaviour
    {
        protected Transform target;
        private AI _ai;
        public event Action OnAttack; 

        private void OnEnable() {
            _ai = GetComponent<AI>();
            if (_ai) {
                _ai.OnTargetChanged += SetTarget;
            }
        }

        private void OnDisable() {
            if (_ai) {
                _ai.OnTargetChanged -= SetTarget;
            }
        }

        private void SetTarget(Transform newTarget) {
            target = newTarget;
        }

        protected void OnAttackInvoker() {
            OnAttack?.Invoke();
        }
        
    }
}
