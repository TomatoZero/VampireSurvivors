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
            var result = context.ReadValue<Vector2>();
            _mousePosition = Camera.main.ScreenToWorldPoint(result);
        }

        public void FirstWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 1);
        }

        public void SecondWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 1);
        }

        public void ThirdWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 1);
        }

        public void FourthWeaponEventHandler(InputAction.CallbackContext context)
        {
            Invoke(context, 1);
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