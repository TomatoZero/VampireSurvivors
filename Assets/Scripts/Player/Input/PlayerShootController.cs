using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2, int> _weaponStartAction;
        [SerializeField] private UnityEvent<Vector2, int> _weaponActivate;
        [SerializeField] private Camera _camera;
        
        
        private Vector2 _mousePosition;

        public void MousePositionEventHandler(InputAction.CallbackContext context)
        {
            var result = context.ReadValue<Vector2>();
            _mousePosition = _camera.ScreenToWorldPoint(result);
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
                _weaponStartAction.Invoke(_mousePosition, weaponNumber);
            }
            else if (context.canceled)
            {
                _weaponActivate.Invoke(_mousePosition, weaponNumber);
            }
        }
    }
}