using Player;
using ScriptableObjects;
using UnityEngine;

namespace PickUpItems.PickUpItemActions.BuffItemActions
{
    public class BuffActionController : PickUpItemActionController
    {
        [SerializeField] private BuffData _buffData;

        private protected override void ItemAction(Collision2D collision2D)
        {
            var buffController = collision2D.gameObject.GetComponent<PlayerBuffController>();
            if (buffController is not null)
            {
                buffController.AddBuff(_buffData);
            }
        }
    }
}