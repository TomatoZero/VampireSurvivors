using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class DamageController : MonoBehaviour, IDamageable, IUpdateStats
    {
        [SerializeField] private UnityEvent<float> _takeDamageEvent;

        private float _armor;

        public void TakeDamage(float damage)
        {
            if(damage - _armor <= 0) return;
            
            _takeDamageEvent.Invoke(damage - _armor);
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _armor = newInstance.GetStatByName(Stats.Stats.Armor).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _armor = newInstance.GetStatByName(Stats.Stats.Armor).Value;
        }
    }
}