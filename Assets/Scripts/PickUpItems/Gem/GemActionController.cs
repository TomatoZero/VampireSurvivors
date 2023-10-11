using Player;
using UnityEngine;

namespace PickUpItems.Gem
{
    public class GemActionController : PickUpItemActionController
    {
        [SerializeField] private GemDataController _gemData;
        
        private protected override void ItemAction(Collision2D collision2D)
        {
            var levelController = collision2D.gameObject.GetComponentInChildren<PlayerLevelController>();
            if (levelController is not null)
            {
                levelController.IncreaseXp(_gemData.XpBonus);
            }
        }
    }
}