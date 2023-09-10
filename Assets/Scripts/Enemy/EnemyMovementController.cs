using System;
using Stats.Instances;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyStatsController))]
    public class EnemyMovementController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private Transform _player;
        private const float DefaultSpeed = 10;

        private Vector2 _nextPosition;
        private Vector2 _moveDirection;

        private float _speed;

        private void Awake()
        {
            _player = transform;
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
            _moveDirection = (_player.position - transform.position).normalized;
            _nextPosition = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_nextPosition);
        }

        public void SetPlayer(Transform player)
        {
            _player = player;
        }

        public void UpdateStatsEventHandler(ObjectStatsInstance newStatsInstance)
        {
            var speedStatPercent = newStatsInstance.GetStatByName(Stats.Stats.Speed).Value;
            _speed = (DefaultSpeed * speedStatPercent) / 100;
        }
    }
}