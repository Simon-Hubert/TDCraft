using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private float _lifeMAX;
    
        float _currentLife;
    
        public float CurrentLife  { get => _currentLife; }
    
        public UnityEvent OnDeath;
        public UnityEvent OnHeal;
        public UnityEvent OnTakeDamage;

        public void TakeDamage(float damage)
        {
            _currentLife -= damage;
            OnTakeDamage?.Invoke();
            if(_currentLife <= 0) OnDeath?.Invoke();
        }

        public void Heal(float heal)
        {
            _currentLife += heal;
            OnHeal?.Invoke();
        }
    }
}
