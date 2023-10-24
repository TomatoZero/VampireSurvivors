using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string, float> _setMoveDirectionAnimationEvent;

        public void MoveDirectionEventHandler(Vector2 moveDirection)
        {
            _setMoveDirectionAnimationEvent.Invoke("Horizontal", moveDirection.x);
        }
    }
}