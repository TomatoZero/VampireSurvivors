using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class DamageController : MonoBehaviour, IDamageable
    {
        [SerializeField] private UnityEvent<float> _takeDamageEvent;
        [SerializeField] private LayerMask _enemyLayer;

        private float _armor;

        public void TakeDamage(float damage)
        {
            if(damage - _armor <= 0) return;
            
            _takeDamageEvent.Invoke(damage - _armor);
        }

        public void UpdateStatsEventHandler(PlayerStats newStats)
        {
            _armor = newStats.GetStatByName(Stats.Stats.Armor).Value;
        }
    }
}