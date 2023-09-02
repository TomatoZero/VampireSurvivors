using Stats.Instances;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [FormerlySerializedAs("_speed")] [SerializeField] private float _defaultSpeed;
        
        private Vector2 _moveDirection;
        private float _speedPlus;

        private void FixedUpdate()
        {
            transform.Translate(_moveDirection.normalized * ((_defaultSpeed + _speedPlus) * Time.fixedDeltaTime));
        }

        public void MoveEventHandler(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }

        public void UpdateStatsEventHandler(PlayerStats newStats)
        {
            float speedStatPercent = newStats.GetStatByName(Stats.Stats.MoveSpeed).Value;
            _speedPlus = (_defaultSpeed * speedStatPercent) / 100;
            
            Debug.Log($"sped plus {_speedPlus}");
        }
    }    
}
