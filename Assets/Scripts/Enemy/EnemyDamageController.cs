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
            Debug.Log($"damage {damage}");
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