using UnityEngine;

namespace PickUpItems
{
    public abstract class PickUpItemActionController : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private PickUpItemMoveController _moveController;


        private void OnCollisionEnter(Collision other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                ItemAction(other);
                _moveController.DestroyItem();
            }
        }

        private protected abstract void ItemAction(Collision collision2D);
    }
}