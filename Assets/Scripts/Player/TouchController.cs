using System;
using PickUpItems.Gem;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class TouchController : MonoBehaviour
    {
        [SerializeField] private LayerMask _pickUpItemLayer;
        [SerializeField] private UnityEvent<int> _xpGainEvent;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (((1 << other.gameObject.layer) & _pickUpItemLayer) != 0)
            {
                CompareTag(other.collider);
            }
        }

        private void CompareTag(Collider2D other)
        {
            if (other.CompareTag("Gem"))
            {
                if (other.TryGetComponent(out GemDataController dataController))
                {
                    _xpGainEvent.Invoke(dataController.XpBonus);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}