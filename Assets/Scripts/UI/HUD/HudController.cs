using System.Collections.Generic;
using Stats.Instances;
using Stats.Instances.Buff;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;

namespace UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> _setCurrentXp;
        [SerializeField] private UnityEvent<TimedBuffInstance> _showBuff;
        [SerializeField] private UnityEvent<List<WeaponInstance>, List<ItemInstance>> _displayItemsEvent;
        
        public void SetCurrentXp(float xp)
        {
            _setCurrentXp.Invoke(xp);
        }

        public void ShowBuff(TimedBuffInstance buff)
        {
            _showBuff.Invoke(buff);
        }

        public void DisplayItemsEvent(List<WeaponInstance> weapons, List<ItemInstance> items)
        {
            _displayItemsEvent.Invoke(weapons, items);
        } 
    }
}