using DefaultNamespace;
using UnityEngine;

namespace PickUpItems.Magnet
{
    public class MagnetActionController : PickUpItemActionController
    {
        [SerializeField] private GemSpawner _gemSpawner;

        public void SetUp(GemSpawner gemSpawner)
        {
            _gemSpawner = gemSpawner;
        }
        
        private protected override void ItemAction(Collision2D collision2D)
        {
            _gemSpawner.MagnetPickUp(collision2D.transform, 25);
        }
    }
}