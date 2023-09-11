using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyDamageController : MonoBehaviour, IDamageable, IUpdateStats
    {
        [SerializeField] private UnityEvent<float> _takeDamageEvent;
        private float _armor;
        
        public void TakeDamage(float damage)
        {
            if(damage - _armor <= 0) return;
            
            _takeDamageEvent.Invoke(damage - _armor);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _armor = newInstance.GetStatByName(Stats.Stats.Armor).Value;
        }
    }
}