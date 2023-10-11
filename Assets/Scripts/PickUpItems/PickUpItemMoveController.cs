using System;
using DefaultNamespace;
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
        
        public delegate void ItemDestroy(GemSpawner.Magnet magnetEvent);

        public event ItemDestroy ItemDestroyEvent;
        
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

        public void DestroyItem()
        {
            ItemDestroyEvent.Invoke(EnableItem);
            UnsubscribeFromEvent();
            Destroy(gameObject);
        }
        
        private void UnsubscribeFromEvent()
        {
            Delegate[] clientList = ItemDestroyEvent.GetInvocationList();
            foreach (var d in clientList)
                ItemDestroyEvent -= (d as ItemDestroy);
        }
    }
}