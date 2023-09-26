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
        [FormerlySerializedAs("_dealDamageEvent")] [SerializeField] private UnityEvent _countdownEvent;
        [FormerlySerializedAs("_stopDamageEvent")] [SerializeField] private UnityEvent _durationEvent;

        private WaitForSeconds _duration;
        private WaitForSeconds _countdown;

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            _duration = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Duration).Value);
            _countdown = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Cooldown).Value);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            StopTimer();
            var weaponInstance = (WeaponInstance)newInstance;

            _duration = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Duration).Value);
            _countdown = new WaitForSeconds(weaponInstance.GetStatByName(Stats.Stats.Cooldown).Value);

            StartTimer();
        }

        public void StartCountdownTimer()
        {
            StartCoroutine(CountdownTimer());
        }
        
        public void StartDurationTimer()
        {
            StartCoroutine(CountdownTimer());
        }

        private void StopTimer()
        {
            _durationEvent.Invoke();
            StopCoroutine(Timer());
            StopCoroutine(CountdownTimer());
            StopCoroutine(DurationTimer());
        }

        private void StartTimer()
        {
            StartCoroutine(Timer());
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
            _countdownEvent.Invoke();
        }

        private IEnumerator DurationTimer()
        {
            yield return _duration;
            _durationEvent.Invoke();
        }
    }
}