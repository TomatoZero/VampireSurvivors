using UnityEngine;

namespace PickUpItems
{
    public class PickUpItemMoveController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _defaultSpeed;

        private Transform _playerTransform;
        private Vector2 _moveDirection;
        private float _speed;

        private void Awake()
        {
            _speed = _defaultSpeed;
        }

        private void FixedUpdate()
        {
            _moveDirection = (_playerTransform.position - transform.position).normalized;
            var newPos = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }

        public void EnableItem(Transform player)
        {
            EnableItem(player, _defaultSpeed);
        }

        public void EnableItem(Transform player, float speed)
        {
            _playerTransform = player;
            _speed = speed;

            this.enabled = true;
        }
    }
}