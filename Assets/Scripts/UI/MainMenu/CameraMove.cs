using UnityEngine;

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
        }

        private Vector3 GetMoveDirection()
        {
            return Random.insideUnitCircle;
        }
    }
}