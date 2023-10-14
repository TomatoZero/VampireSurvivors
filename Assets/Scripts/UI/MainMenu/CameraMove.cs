using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.MainMenu
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _moveDirection;

        private void FixedUpdate()
        {
            var newPos = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _moveDirection = GetMoveDirection();
            Debug.Log(_moveDirection);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _moveDirection = GetMoveDirection();
            Debug.Log(_moveDirection);
        }

        private Vector3 GetMoveDirection()
        {
            var newDirection = new Vector3(Random.Range(-1,1), Random.Range(-1,1));

            if (newDirection.Equals(Vector2.zero))
            {
                return -_moveDirection;
            }
            
            return newDirection;
        }
    }
}