using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2, int> _weaponStartAction;
        [SerializeField] private UnityEvent<Vector2, int> _weaponActivate;
        
        private Vector2 _mousePosition;

        public void MousePositionEventHandler(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
        }

        public void FirstWeaponEventHandler(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _weaponStartAction.Invoke(_mousePosition, 1);
            }
            else if (context.canceled)
            {
                _weaponActivate.Invoke(_mousePosition, 1);            
            }
        }

        public void SecondWeaponEventHandler(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _weaponStartAction.Invoke(_mousePosition, 2);
            }
            else if (context.canceled)
            {
                _weaponActivate.Invoke(_mousePosition, 2);            
            }
        }

        public void ThirdWeaponEventHandler(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _weaponStartAction.Invoke(_mousePosition, 3);
            }
            else if (context.canceled)
            {
                _weaponActivate.Invoke(_mousePosition, 3);            
            }
        }

        public void FourthWeaponEventHandler(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _weaponStartAction.Invoke(_mousePosition, 4);
            }
            else if (context.canceled)
            {
                _weaponActivate.Invoke(_mousePosition, 4);            
            }
        }
    }
}