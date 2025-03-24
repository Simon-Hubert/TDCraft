using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class CloseRangeAttack : Attack
    {
        private bool _canStrike = true;
        [SerializeField] private float _cooldown;
        
        private void FixedUpdate() {
            if (IsTargetInRange()) {
                if(_canStrike) Strike();
            }
        }
        
        private void Strike() {
            _canStrike = false;
            Debug.Log("Attacked");
            StartCoroutine(Reload());
        }
        
        IEnumerator Reload() {
            yield return new WaitForSeconds(_cooldown);
            _canStrike = true;
        }
    }
}
