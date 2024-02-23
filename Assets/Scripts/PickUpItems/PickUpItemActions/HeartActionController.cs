using Player;
using UnityEngine;

namespace PickUpItems.RegenHp
{
    public class HeartActionController : PickUpItemActionController
    {
        [SerializeField] private float _restoreHp;

        private protected override void ItemAction(Collision2D collision2D)
        {
            var reference = collision2D.gameObject.GetComponentInChildren<PlayerReference>();
            if (reference is not null)
            {
                reference.HealthController.Heal(_restoreHp);
            }
        }

        public void Setup(float hpRestore)
        {
            _restoreHp = hpRestore;
        }
    }
}