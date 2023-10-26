using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(EnemyStatsController))]
    public class EnemyMovementController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private UnityEvent<Vector2> _moveDirectionEvent;
        
        private Transform _player;

        private Vector2 _nextPosition;
        private Vector2 _moveDirection;

        private float _speed;

        private void Awake()
        {
            _player = transform;
        }

        private void FixedUpdate()
        {
            _moveDirection = (_player.position - transform.position).normalized;
            _nextPosition = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_nextPosition);
            _moveDirectionEvent.Invoke(_moveDirection);
        }

        public void SetPlayer(Transform player)
        {
            _player = player;
        }

        public void Die()
        {
            _speed = 0;
        }
        
        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _speed = newInstance.GetStatByName(Stats.Stats.MoveSpeed).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _speed = newInstance.GetStatByName(Stats.Stats.MoveSpeed).Value;
        }
    }
}