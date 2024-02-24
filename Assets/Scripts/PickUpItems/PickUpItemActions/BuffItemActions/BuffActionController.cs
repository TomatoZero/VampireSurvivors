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
            Debug.Log($"{gameObject.name} {collision2D.gameObject.name}");
            if (reference is not null)
            {
                Debug.Log($"{gameObject.name} found reference");
                reference.BuffController.AddBuff(_buffData);
            }
        }
    }
}