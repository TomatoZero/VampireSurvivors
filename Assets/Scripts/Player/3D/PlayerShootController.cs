using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player._3D
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector3, int> _weaponStartAction;
        [SerializeField] private UnityEvent<Vector3, int> _weaponActivate;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _groundLayerMask;
        
        private Vector2 _mousePosition;
        private Vector3 _position;

        public void MousePositionEventHandler(InputAction.CallbackContext context)
        {
            var result = context.ReadValue<Vector2>();

            var ray = _camera.ScreenPointToRay(result);

            if (Physics.Raycast(ray, out var hit ,100f, _groundLayerMask))
            {
                _position = hit.point;
            }
        }

        public void JoystickLookEventHandler(InputAction.CallbackContext context)
        {
            var result = context.ReadValue<Vector2>();
            var aimPosition = new Vector3(result.x, 0f, result.y);

            _position = aimPosition;
            
            Debug.Log($"_position {_position}");
        }

        public void FirstWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 1);
        }

        public void SecondWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 2);
        }

        public void ThirdWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 3);
        }

        public void FourthWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 4);
        }

        private void Invoke(InputAction.CallbackContext context, int weaponNumber)
        {
            if (context.started)
            {
                _weaponStartAction.Invoke(_position, weaponNumber);
            }
            else if (context.canceled)
            {
                _weaponActivate.Invoke(_position, weaponNumber);
            }
        }
    }
}