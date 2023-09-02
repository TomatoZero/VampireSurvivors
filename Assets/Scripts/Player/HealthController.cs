using System;
using System.Collections;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> _playerHealthChangeEvent;
        [SerializeField] private UnityEvent _playerDie;
        
        private float _maxHealth;
        private float _recovery;    //per second

        private float _currentHealth;
        private bool _isRecovering = false;

        private void Start()
        {
            _currentHealth = _maxHealth;
            InvokeHealthChaneEvent();
        }

        public void Heal(float hp)
        {
            if (hp < 0) throw new ArgumentException();
            if (Math.Abs(_currentHealth - _maxHealth) < 0)
            {
                TryTurnOfRecover();
                return;
            }

            _currentHealth += hp;

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;

            InvokeHealthChaneEvent();
        }
        
        public void TakeDamage(float hp)
        {
            if (hp < 0) throw new ArgumentException();
            if (_currentHealth <= 0) throw new Exception("Player hp less or equal zero");
            
            _currentHealth -= hp;
            TryTurnOnRecover();

            if(_currentHealth <= 0) _playerDie.Invoke();

            InvokeHealthChaneEvent();
        }
        
        public void UpdateStatsEventHandler(PlayerStats newStats)
        {
            _maxHealth = newStats.GetStatByName(Stats.Stats.MaxHealth).Value;
            _recovery = newStats.GetStatByName(Stats.Stats.Recovery).Value;

            InvokeHealthChaneEvent();
            
            Debug.Log($"_maxHealth {_maxHealth} _recovery {_recovery}");
        }

        private void TryTurnOfRecover()
        {
            if(!_isRecovering || _recovery == 0) return;
            
            StopCoroutine(RecoverHpPerSecond());
            _isRecovering = false;
        }

        private void TryTurnOnRecover()
        {
            if(_isRecovering || _recovery == 0) return;
            
            StartCoroutine(RecoverHpPerSecond());
            _isRecovering = true;
        }
        
        private IEnumerator RecoverHpPerSecond()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                Heal(_recovery);
            }
        }

        private void InvokeHealthChaneEvent()
        {
            // Debug.Log($"{_currentHealth} / {_maxHealth} = {_currentHealth / _maxHealth}");
            
            _playerHealthChangeEvent.Invoke(_currentHealth / _maxHealth);
        }
    }
}