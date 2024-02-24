using Player;
using UnityEngine;

namespace PickUpItems.Gem
{
    public class GemActionController : PickUpItemActionController
    {
        private int _xpBonus = 5;

        private protected override void ItemAction(Collision collision2D)
        {
            var reference = collision2D.gameObject.GetComponentInChildren<PlayerReference>();
            if (reference is not null)
            {
                Debug.Log($"ex++");
                reference.LevelController.IncreaseXp(_xpBonus);
            }
        }

        public void Setup(int xpBonus)
        {
            _xpBonus = xpBonus;
        }
    }
}