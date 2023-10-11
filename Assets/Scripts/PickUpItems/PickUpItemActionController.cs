using UnityEngine;

namespace PickUpItems
{
    public abstract class PickUpItemActionController : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                ItemAction(other);
            }
        }

        private protected abstract void ItemAction(Collision2D collision2D);
    }
}