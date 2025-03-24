using System;
using System.IO;
using UnityEngine;

namespace Entities
{
    public class BasicProjectile : Projectile
    {
        [SerializeField] private float _lifeTime;
        private Vector2 direction = Vector2.zero;
        
        public override void Init(Transform initTarget, int initDamages, bool ownedByPlayer) {
            base.Init(initTarget, initDamages, ownedByPlayer);
            direction = initTarget.position - transform.position;
            direction.Normalize();
            Destroy(gameObject, _lifeTime);
        }

        private void Update() {
            transform.position += (Vector3)direction * (speed * Time.deltaTime);
        }
    }
}
