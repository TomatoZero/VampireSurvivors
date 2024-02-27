using Player;
using UnityEngine;

namespace PickUpItems.RegenHp
{
    public class HeartActionController : PickUpItemActionController
    {
        [SerializeField] private float _restoreHp;
        
        private protected override void ItemAction(Collision2D collision2D)
        {
            var healthController = collision2D.gameObject.GetComponentInChildren<HealthController>();
            if (healthController is not null)
            {
                healthController.Heal(_restoreHp);
            }
        }
        
        public void Setup(float hpRestore)
        {
            _restoreHp = hpRestore;
        }
    }
}