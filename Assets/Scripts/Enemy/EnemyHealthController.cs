using System;
using DefaultNamespace;
using Interface;
using Spawner;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealthController : MonoBehaviour, IUpdateStats, IHealth
    {
        [SerializeField] private UnityEvent<Vector3> _enemyDie;
        
        private float _maxHealth;
        private float _currentHealth;

        public UnityEvent<Vector3> EnemyDie => _enemyDie;

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

            if(_currentHealth <= 0) _enemyDie.Invoke(transform.position);
        }

        public void Kill()
        {
            transform.parent.gameObject.SetActive(false);
            Destroy(transform.parent.gameObject);
        }

        public void AddDieListener(GemSpawner.SpawnGem method)
        {
            _enemyDie.AddListener(_ => method(transform.position));
        }
    }
}