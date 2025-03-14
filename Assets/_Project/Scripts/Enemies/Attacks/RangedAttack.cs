using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    public class RangedAttack : Attack
    {
        [SerializeField] private float _range;
        [SerializeField] private int _damages;
        [SerializeField] private float _fireRate;
        [SerializeField] private Projectile _projectile;
        private bool _canShoot = true;

        private void Update() {
            if (((Vector2)(target.position - transform.position)).magnitude <= _range) {
                if (_canShoot) Shoot();
            }
        }
        private void Shoot() {
            _canShoot = false;
            Projectile proj = Instantiate(_projectile, transform.position, Quaternion.identity);
            proj.Init(target, _damages, false);
            OnAttackInvoker();
            StartCoroutine(Reload());
        }

        IEnumerator Reload() {
            yield return new WaitForSeconds(1.0f / _fireRate);
            _canShoot = true;
        }

        private void OnDrawGizmos() {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, new Vector3(0,0,1), _range);
        }
    }
}
