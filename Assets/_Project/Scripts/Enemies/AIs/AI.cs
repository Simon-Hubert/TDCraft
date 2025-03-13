using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class AI : MonoBehaviour
    {
        private Sense[] _senses;
        protected readonly List<Vector2> targets = new List<Vector2>();
        protected EnemyController controller;

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

        protected abstract Vector2 ChooseTarget();

        private void Start() {
            controller = GetComponent<EnemyController>();
        }

        protected virtual void Update() {
            Vector2 dir = ChooseTarget() - (Vector2)transform.position;
            targets.Clear();
            dir.Normalize();
            controller?.Move(dir);
        }
    }
}
