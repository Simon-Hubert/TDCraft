using System;
using UnityEngine;

namespace Enemies
{
    public abstract class Attack : MonoBehaviour
    {
        public event Action<Transform> OnTargetEnterRange;
        public event Action<Transform> OnTargetExitRange;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Debug.Log("Collision enter");
                OnTargetEnterRange?.Invoke(other.transform);
            } 
        }
        
        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                OnTargetExitRange?.Invoke(other.transform);
                Debug.Log("Collision exit");
            }
        }

        public abstract void AttackTarget();
    }
}
