using System;
using UnityEngine;

namespace Entities
{
    public class SixthSense : Sense
    {
        [SerializeField] private GameObject[] _targets;

        private void FixedUpdate() {
            foreach (GameObject target in _targets) {
                SensedAt(target.transform);
            }
        }
    }
}
