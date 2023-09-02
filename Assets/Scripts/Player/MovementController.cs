using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector2 _moveDirection;

        private void FixedUpdate()
        {
            transform.Translate(_moveDirection.normalized * (_speed * Time.fixedDeltaTime));
        }

        public void MoveEventHandler(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
    }    
}
