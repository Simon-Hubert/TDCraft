using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public abstract class AI : MonoBehaviour
    {
        private Sense[] _senses;
        protected readonly List<Transform> targets = new List<Transform>();
        protected Transform _mainTarget;
        private EnemyController controller;

        public event Action<Transform> OnTargetChanged;

        private void OnEnable() {
            _senses = GetComponents<Sense>();
            if (_senses != null) {
                foreach (Sense sense in _senses) {
                    sense.OnSenseTarget += targets.Add;
                }
            }
        }

        private void OnDisable() {
            if (_senses != null) {
                foreach (Sense sense in _senses) {
                    sense.OnSenseTarget -= targets.Add;
                }
            }
        }

        protected abstract Transform ChooseTarget();

        protected abstract Vector2 ChooseDestination();

        protected virtual void Start() {
            controller = GetComponent<EnemyController>();
        }

        private void FixedUpdate() {
            // TODO Refactor, y a un soucis avec ça car tout a besoin d'être sur les mêmes updates donc c'est chiant
            // c'est les sense qui doivent changer i guess
            Transform newTarget = ChooseTarget();
            if (newTarget != _mainTarget) {
                OnTargetChanged?.Invoke(newTarget);
                _mainTarget = newTarget;
            }
            
            targets.Clear();
            Vector2 dir = ChooseDestination() - (Vector2)transform.position;
            dir.Normalize();
            controller?.Move(dir);
        }
    }
}
