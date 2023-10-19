using Player;
using UnityEngine;

namespace PickUpItems.Invincibility
{
    public class InvincibleActionController : PickUpItemActionController
    {
        [SerializeField] private float _invincibleTime;
        
        private protected override void ItemAction(Collision2D collision2D)
        {
            var damageController = collision2D.gameObject.GetComponentInChildren<DamageController>();
            if (damageController is not null)
            {
                damageController.MakeInvincibleFor(_invincibleTime);
            }
        }

        public void Setup(float seconds)
        {
            _invincibleTime = seconds;
        }
    }
}