using Player;
using UnityEngine;

namespace PickUpItems.Gem
{
    public class GemActionController : PickUpItemActionController
    {
        private int _xpBonus = 5;

        private protected override void ItemAction(Collision2D collision2D)
        {
            var reference = collision2D.gameObject.GetComponentInChildren<PlayerReference>();
            if (reference is not null)
            {
                reference.LevelController.IncreaseXp(_xpBonus);
            }
        }

        public void Setup(int xpBonus)
        {
            _xpBonus = xpBonus;
        }
    }
}