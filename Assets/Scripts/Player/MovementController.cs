using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _moveDirection;
        private float _speed;

        private void FixedUpdate()
        {
            var newPos = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
        }

        public void MoveEventHandler(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
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