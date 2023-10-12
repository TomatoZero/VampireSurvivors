using System;
using System.Collections;
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
        private bool _isInvincible;

        private void OnEnable()
        {
            _isInvincible = false;
        }

        public void TakeDamage(float damage)
        {
            if(_isInvincible) return;
            if(damage - _armor <= 0) return;
            
            _takeDamageEvent.Invoke(damage - _armor);
        }

        public void MakeInvincibleFor(float seconds)
        {
            _isInvincible = true;
            StartCoroutine(BecomeNotInvincibleAfter(seconds));
        }
        
        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _armor = newInstance.GetStatByName(Stats.Stats.Armor).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _armor = newInstance.GetStatByName(Stats.Stats.Armor).Value;
        }

        private IEnumerator BecomeNotInvincibleAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _isInvincible = false;
        }
    }
}