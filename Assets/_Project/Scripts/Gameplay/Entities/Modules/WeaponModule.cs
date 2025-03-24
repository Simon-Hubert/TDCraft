using UnityEngine;

namespace Entities
{
    public class WeaponModule : ControllableModule
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private int _damages;
        [SerializeField] private float _fireRate;
        
        //TODO Implement shoot and reload
    }
}
