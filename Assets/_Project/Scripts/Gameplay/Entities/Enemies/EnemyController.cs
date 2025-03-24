using System;
using UnityEngine;

namespace Entities
{       
    //TODO On devra peut etre passer sur une state machine pour le jeu final
    
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _friction;
        private Vector2 _velocity = Vector2.zero;
        private Vector2 _moveInput = Vector2.zero;
        
        public void Move(Vector2 input) {
            _moveInput = input;
        }

        private void Update() {
            Vector2 accel = _moveInput * _moveSpeed - _velocity*_friction;
            _velocity += accel * Time.deltaTime;
            transform.position += (Vector3)_velocity * Time.deltaTime;
        }
    }
}
