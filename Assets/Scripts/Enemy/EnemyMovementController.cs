using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyMovementController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private UnityEvent<Vector2> _moveDirectionEvent;
        
        [SerializeField] private Transform _player;

        private Vector3 _nextPosition;
        private Vector3 _moveDirection;

        private float _speed;
        private float _distanceToPlayer;

        public float DistanceToPlayer => _distanceToPlayer;

        public Transform Player => _player;

        private void Awake()
        {
            // _player = transform;
        }

        private void FixedUpdate()
        {
            var vectorToPlayer = (_player.position - transform.position);
            _distanceToPlayer = vectorToPlayer.magnitude;
            _moveDirection = vectorToPlayer.normalized;
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