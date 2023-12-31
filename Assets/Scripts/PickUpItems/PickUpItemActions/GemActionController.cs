﻿using Player;
using UnityEngine;

namespace PickUpItems.Gem
{
    public class GemActionController : PickUpItemActionController
    {
        private int _xpBonus = 5;
        
        private protected override void ItemAction(Collision2D collision2D)
        {
            var levelController = collision2D.gameObject.GetComponentInChildren<PlayerLevelController>();
            if (levelController is not null)
            {
                levelController.IncreaseXp(_xpBonus);
            }
        }
        
        public void Setup(int xpBonus)
        {
            _xpBonus = xpBonus;
        }
    }
}