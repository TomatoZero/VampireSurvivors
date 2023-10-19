using System;
using System.Collections.Generic;
using System.Linq;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;

namespace UI.HUD
{
    public class CurrentItemsDisplayController : MonoBehaviour
    {
        [SerializeField] private List<ItemDisplayController> _displayWeapons;
        [SerializeField] private List<ItemDisplayController> _displayItems;

        private Queue<ItemDisplayController> _freeWeaponsDisplay;
        private Queue<ItemDisplayController> _freeItemsDisplay;

        private void Awake()
        {
            _freeWeaponsDisplay = new Queue<ItemDisplayController>();
            _freeItemsDisplay = new Queue<ItemDisplayController>();

            foreach (var display in _displayWeapons) _freeWeaponsDisplay.Enqueue(display);
            foreach (var display in _displayItems) _freeItemsDisplay.Enqueue(display);
        }

        public void SetItems(List<WeaponInstance> weapons, List<ItemInstance> items)
        {
            if(weapons is not null) SetupItems(weapons?.Cast<ObjectInstance>().ToList(), _displayWeapons, _freeWeaponsDisplay);
            if(items is not null) SetupItems(items.Cast<ObjectInstance>().ToList(), _displayItems, _freeItemsDisplay);
        }

        private void SetupItems(List<ObjectInstance> items, List<ItemDisplayController> display,
            Queue<ItemDisplayController> allFreeDisplay)
        {
            foreach (var item in items)
            {
                if(item is null) continue;
                var found = false;
                
                foreach (var displayController in display)
                {
                    if (displayController.Instance == item)
                    {
                        displayController.UpdateLevel(item.CurrentLvl);
                        found = true;
                        break;
                    }
                }

                if (found) continue;
                
                if (allFreeDisplay.TryDequeue(out ItemDisplayController freeDisplay))
                {
                    freeDisplay.Set(item);
                }
                else
                {
                    throw new Exception("To many items");
                }
            }
        }
    }
}