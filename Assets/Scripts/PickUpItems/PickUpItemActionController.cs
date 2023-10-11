using UnityEngine;

namespace PickUpItems
{
    public abstract class PickUpItemActionController : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private PickUpItemMoveController _moveController;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                ItemAction(other);
                _moveController.DestroyItem();
            }
        }

        private protected abstract void ItemAction(Collision2D collision2D);
    }
}