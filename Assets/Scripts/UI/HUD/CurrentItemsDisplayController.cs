using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;

namespace UI.HUD
{
    public class CurrentItemsDisplayController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<ObjectInstance> _displayWeaponsEvent;
        [SerializeField] private UnityEvent<ObjectInstance> _displayItemsEvent;

        public void SetItems(WeaponInstance weapons, ObjectInstance items)
        {
            _displayWeaponsEvent.Invoke(weapons);
            _displayItemsEvent.Invoke(items);
        }
    }
}