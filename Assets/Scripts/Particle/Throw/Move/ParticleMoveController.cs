using Interface;
using Stats.Instances;
using UnityEngine;

namespace Particle.Throw.Move
{
    public class ParticleMoveController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Rigidbody _rigidbody;

        private float _speed;

        private Vector3 _moveDirection;
        private Vector3 _targetPos;

        private void FixedUpdate()
        {
            var newPos = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }

        public void Move(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _speed = newInstance.GetStatByName(Stats.Stats.ProjectilesSpeed).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _speed = newInstance.GetStatByName(Stats.Stats.ProjectilesSpeed).Value;
        }
    }
}