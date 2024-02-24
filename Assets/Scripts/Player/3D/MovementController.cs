using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player._3D
{
    public class MovementController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private UnityEvent<Vector2> _moveEvent;

        private Vector3 _moveDirection;
        private Vector3 _moveDirection2D;
        private float _speed = 5;

        private void FixedUpdate()
        {
            var newPos = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
            
            Debug.Log($"newPos {newPos}");
            
            _rigidbody.MovePosition(newPos);
            _moveEvent.Invoke(_moveDirection2D);
        }

        public void MoveEventHandler(InputAction.CallbackContext context)
        {
            _moveDirection2D = context.ReadValue<Vector2>();
            _moveDirection = new Vector3(_moveDirection2D.x, 0, _moveDirection2D.y);
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            // _speed = newInstance.GetStatByName(Stats.Stats.MoveSpeed).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            // _speed = newInstance.GetStatByName(Stats.Stats.MoveSpeed).Value;
        }
    }
}