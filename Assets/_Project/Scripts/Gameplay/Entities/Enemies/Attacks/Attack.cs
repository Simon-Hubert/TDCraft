using System;
using UnityEditor;
using UnityEngine;

namespace Entities
{
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField] protected float range;
        [SerializeField] protected int damages;

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

        public bool IsTargetInRange() {
            if (!target) return false;
            return ((Vector2)(target.position - transform.position)).magnitude <= range;
        }
        
        private void SetTarget(Transform newTarget) {
            target = newTarget;
        }

        protected void OnAttackInvoker() {
            OnAttack?.Invoke();
        }
        
        
        private void OnDrawGizmos() {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, new Vector3(0,0,1), range);
        }
        
    }
}
