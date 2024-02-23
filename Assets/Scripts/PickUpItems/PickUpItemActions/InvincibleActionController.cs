using Player;
using UnityEngine;

namespace PickUpItems.Invincibility
{
    public class InvincibleActionController : PickUpItemActionController
    {
        [SerializeField] private float _invincibleTime;

        private protected override void ItemAction(Collision2D collision2D)
        {
            var reference = collision2D.gameObject.GetComponentInChildren<PlayerReference>();
            if (reference is not null)
            {
                reference.DamageController.MakeInvincibleFor(_invincibleTime);
            }
        }

        public void Setup(float seconds)
        {
            _invincibleTime = seconds;
        }
    }
}