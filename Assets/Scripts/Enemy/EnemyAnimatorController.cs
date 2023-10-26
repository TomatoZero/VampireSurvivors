using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string, float> _setMoveDirectionAnimationEvent;
        [SerializeField] private UnityEvent<string, bool> _batDieEvent;

        private Vector2 _prevDirection = Vector2.down;

        public void MoveDirectionEventHandler(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
            {
                _setMoveDirectionAnimationEvent.Invoke("Horizontal", moveDirection.x);
                _prevDirection = moveDirection;
            }
            else
            {
                _setMoveDirectionAnimationEvent.Invoke("Horizontal", _prevDirection.x);
            }
        }

        public void DieEventHandler()
        {
            _batDieEvent.Invoke("Die", true);
        }
    }
}