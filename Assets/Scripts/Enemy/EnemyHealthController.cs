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

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _maxHealth = newInstance.GetStatByName(Stats.Stats.MaxHealth).Value;
            _currentHealth = _maxHealth;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _maxHealth = newInstance.GetStatByName(Stats.Stats.MaxHealth).Value;
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
            if (_currentHealth <= 0)
            {
                Debug.LogWarning("Enemy hp less or equal zero");
                return;
            }
            
            _currentHealth -= damage;

            if(_currentHealth <= 0) _enemyDie.Invoke();
        }

        public void Die()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}