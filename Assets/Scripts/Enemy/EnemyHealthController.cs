using System;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealthController : MonoBehaviour, IUpdateStats, IHealth
    {
        [SerializeField] private UnityEvent _enemyDie;
        
        private float _maxHealth;
        private float _currentHealth;
        
        public void UpdateStatsEventHandler(ObjectStatsInstance newStatsInstance)
        {
            _maxHealth = newStatsInstance.GetStatByName(Stats.Stats.MaxHealth).Value;
        }
        
        public void Heal(float hp)
        {
            if (hp < 0) throw new ArgumentException();
            if (Math.Abs(_currentHealth - _maxHealth) < 0) return;

            _currentHealth += hp;

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0) throw new ArgumentException();
            if (_currentHealth <= 0) throw new Exception("Enemy hp less or equal zero");
            
            _currentHealth -= damage;

            if(_currentHealth <= 0) _enemyDie.Invoke();
        }
    }
}