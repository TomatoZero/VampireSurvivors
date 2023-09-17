using System;
using System.Collections;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Melee
{
    public class MeleeWeaponTimer : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private UnityEvent _dealDamageEvent;
        [SerializeField] private UnityEvent _stopDamageEvent;

        private WaitForSeconds _duration;
        private float _defaultDuration;

        private WaitForSeconds _countdown;
        private float _defaultCountdown;

        private void Start()
        {
            
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            StopTimer();
            var weaponInstance = (WeaponInstance)newInstance;

            SetStat(out _duration, _defaultDuration, weaponInstance.GetStatByName(Stats.Stats.Duration).Value);
            SetStat(out _countdown, _defaultCountdown, weaponInstance.GetStatByName(Stats.Stats.Cooldown).Value);
            
            StartTimer();
        }

        private void SetStat(out WaitForSeconds variable, float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            variable = new WaitForSeconds(defaultValue + addValue);
        }

        private void StopTimer()
        {
            _stopDamageEvent.Invoke();
            StopCoroutine(Timer());
        }

        private void StartTimer()
        {
            StopCoroutine(Timer());
        }
        
        private IEnumerator Timer()
        {
            while (true)
            {
                _dealDamageEvent.Invoke();
                yield return _duration;
                _stopDamageEvent.Invoke();
                yield return _countdown;
            }
        }
    }
}