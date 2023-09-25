using System.Collections;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public class WeaponTimer : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private UnityEvent _dealDamageEvent;
        [SerializeField] private UnityEvent _stopDamageEvent;

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

        private void StopTimer()
        {
            _stopDamageEvent.Invoke();
            StopCoroutine(Timer());
        }

        private void StartTimer()
        {
            StartCoroutine(Timer());
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

        private IEnumerator CountdownTimer()
        {
            yield return _countdown;
            _dealDamageEvent.Invoke();
        }
    }
}