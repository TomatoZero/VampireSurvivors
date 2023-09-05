using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class DamageController : MonoBehaviour, IDamageable, IUpdateStats
    {
        [SerializeField] private UnityEvent<float> _takeDamageEvent;
        [SerializeField] private LayerMask _enemyLayer;

        private float _armor;

        public void TakeDamage(float damage)
        {
            if(damage - _armor <= 0) return;
            
            _takeDamageEvent.Invoke(damage - _armor);
        }

        public void UpdateStatsEventHandler(ObjectStatsInstance newStatsInstance)
        {
            _armor = newStatsInstance.GetStatByName(Stats.Stats.Armor).Value;
        }
    }
}