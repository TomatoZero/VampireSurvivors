using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Vector2 _prevDirection = Vector2.down;
        
        public void SetMoveAnimationEventHandler(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
            {
                _animator.SetFloat("Horizontal", moveDirection.x);
                _animator.SetFloat("Vertical", moveDirection.y);
                _animator.SetFloat("Speed", 1);
                _prevDirection = moveDirection;
            }
            else
            {
                _animator.SetFloat("Horizontal", _prevDirection.x);
                _animator.SetFloat("Vertical", _prevDirection.y);
                _animator.SetFloat("Speed", 0);
            }
        }
    }
}