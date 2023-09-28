using System;
using System.Collections;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Weapons
{
    public class WeaponTimer : MonoBehaviour, IUpdateStats
    {
        [FormerlySerializedAs("_dealDamageEvent")] [SerializeField]
        private UnityEvent _countdownEvent;

        [FormerlySerializedAs("_stopDamageEvent")] [SerializeField]
        private UnityEvent _durationEvent;

        private WaitForSeconds _duration;
        private WaitForSeconds _countdown;

        private Coroutine _countdownCoroutine;
        private Coroutine _durationCoroutine;
        private Coroutine _timerCoroutine;
        
        private bool _isCountdownActive = false;
        private bool _isDurationActive = false;
        private bool _isTimerActive = false;
        
        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            _duration = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Duration).Value);
            _countdown = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Cooldown).Value);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            StopActiveCoroutine();
            var weaponInstance = (WeaponInstance)newInstance;

            _duration = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Duration).Value);
            _countdown = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Cooldown).Value);

            ResumeActiveCoroutine();
        }

        public void StartCountdownTimer()
        {
            if (_isCountdownActive) return;

            _isCountdownActive = true;
            _countdownCoroutine = StartCoroutine(CountdownTimer());
        }

        public void StartDurationTimer()
        {
            if(_isDurationActive) return;
            
            _isDurationActive = true;
            _durationCoroutine = StartCoroutine(DurationTimer());
        }

        public void StartTimer()
        {
            if(_isTimerActive) return;
            
            _isTimerActive = true;
            _timerCoroutine = StartCoroutine(Timer());
        }

        private void StopActiveCoroutine()
        {
            _durationEvent.Invoke();

            if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
            if (_durationCoroutine != null) StopCoroutine(_durationCoroutine);
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
        }

        private void ResumeActiveCoroutine()
        {
            if (_isCountdownActive)
            {
                _countdownCoroutine = StartCoroutine(CountdownTimer());
            }
            
            if (_isDurationActive)
            {
                _durationCoroutine = StartCoroutine(DurationTimer());
            }
            
            if (_isTimerActive)
            {
                _timerCoroutine = StartCoroutine(Timer());
            }
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _countdownEvent.Invoke();
                yield return _duration;
                _durationEvent.Invoke();
                yield return _countdown;
            }
        }

        private IEnumerator CountdownTimer()
        {
            yield return _countdown;
            
            _isCountdownActive = false;
            _countdownEvent.Invoke();
        }

        private IEnumerator DurationTimer()
        {
            yield return _duration;
            
            _isDurationActive = false;
            _durationEvent.Invoke();
        }
    }
}