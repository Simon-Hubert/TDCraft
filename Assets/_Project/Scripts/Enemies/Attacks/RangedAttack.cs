using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class RangedAttack : Attack
    {
        [SerializeField] private float _fireRate;
        [SerializeField] private Projectile _projectile;
        private bool _canShoot = true;

        private void FixedUpdate() {
            if (IsTargetInRange()) {
                if (_canShoot) Shoot();
            }
        }
        private void Shoot() {
            _canShoot = false;
            Projectile proj = Instantiate(_projectile, transform.position, Quaternion.identity);
            proj.Init(target, damages, false);
            OnAttackInvoker();
            StartCoroutine(Reload());
        }
        

        IEnumerator Reload() {
            yield return new WaitForSeconds(1.0f / _fireRate);
            _canShoot = true;
        }
        
    }
}
