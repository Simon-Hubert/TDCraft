using System;
using UnityEngine;

namespace Entities
{
    public abstract class Projectile : MonoBehaviour
    {
        protected Transform target;
        private int damages;
        [SerializeField] protected float speed;
        private string _tag;
        private bool _initialized = false;

        public virtual void Init(Transform initTarget, int initDamages, bool ownedByPlayer) {
            target = initTarget;
            damages = initDamages;
            _initialized = true;
            _tag = ownedByPlayer ? "Player" : "Ennemy";
        }

        private void Start() {
            if (!_initialized) {
                Debug.LogError("Projectiled spawned but not initialized");
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag(_tag)) {
                //TODO other.GetComponent<HealthProxy>().TakeDamage(_damages)
            }
        }
    }
}
