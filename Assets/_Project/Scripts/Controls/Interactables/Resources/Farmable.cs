using System;
using Controls;
using UnityEngine;
using UnityEngine.Events;

namespace Controls
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Farmable : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected SO_FarmableType _farmableType;

        #endregion

        #region Variables

        protected float _currentLife;

        #endregion

        #region UnityEvents

        public UnityEvent OnDamaged;
        public UnityEvent OnDropResource;

        #endregion

        private void Awake()
        {
            _currentLife = _farmableType.LifeMAX;
            GetComponentInChildren<SpriteRenderer>().sprite = _farmableType.Sprite;
        }

        public virtual void DropResource()
        {
            Instantiate(_farmableType.DroppedResource, transform.position, Quaternion.identity);
            OnDropResource?.Invoke();
            DestroyFarmable();
        }

        public virtual void DestroyFarmable()
        {
            Destroy(gameObject);
        }
    }
}
    
