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
            var reference = collision2D.gameObject.GetComponent<PlayerReference>();
            if (reference is not null)
            {
                reference.BuffController.AddBuff(_buffData);
            }
        }
    }
}