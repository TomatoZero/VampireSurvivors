using System.Collections.Generic;
using Stats.Instances;
using Stats.Instances.Buff;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> _setCurrentXp;
        [SerializeField] private UnityEvent<TimedBuffInstance> _showBuff;
        [SerializeField] private UnityEvent<List<WeaponReferences>, List<ItemInstance>> _displayItemsEvent;
        
        public void SetCurrentXp(float xp)
        {
            _setCurrentXp.Invoke(xp);
        }

        public void ShowBuff(TimedBuffInstance buff)
        {
            _showBuff.Invoke(buff);
        }

        public void DisplayItemsEvent(List<WeaponReferences> weapons, List<ItemInstance> items)
        {
            _displayItemsEvent.Invoke(weapons, items);
        } 
    }
}