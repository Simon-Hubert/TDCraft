using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class AI : MonoBehaviour
    {
        private Sense[] _senses;
        protected readonly List<Transform> targets = new List<Transform>();
        private Transform _mainTarget;
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

        private void Start() {
            controller = GetComponent<EnemyController>();
        }

        private void Update() {
            Transform newTarget = ChooseTarget();
            if (newTarget != _mainTarget) {
                OnTargetChanged?.Invoke(newTarget);
                _mainTarget = newTarget;
            }
            
            targets.Clear();
            Vector2 dir = (_mainTarget.position - transform.position);
            dir.Normalize();
            controller?.Move(dir);
        }
    }
}
