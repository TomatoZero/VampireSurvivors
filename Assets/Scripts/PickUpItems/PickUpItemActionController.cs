using UnityEngine;

namespace PickUpItems
{
    public abstract class PickUpItemActionController : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private PickUpItemMoveController _moveController;


        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"{gameObject.name} {other.gameObject.name}");
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                Debug.Log($"++++++++++++ {gameObject.name} {other.gameObject.name}");
                ItemAction(other);
                _moveController.DestroyItem();
            }
        }

        private protected abstract void ItemAction(Collision2D collision2D);
    }
}