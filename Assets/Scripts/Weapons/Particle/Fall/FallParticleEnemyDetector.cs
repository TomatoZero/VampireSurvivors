using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Particle.Fall
{
    public class FallParticleEnemyDetector : ParticleEnemyDetector
    {
        [SerializeField] private protected UnityEvent<Collider[]> _hitEnemyEvent;
        
        private Coroutine _damageCoroutine;

        private void OnEnable()
        {
            TryStartCoroutine();
        }

        private void OnDisable()
        {
            StopCoroutine(_damageCoroutine);
        }

        private void TryStartCoroutine()
        {
            if (_damageCoroutine is not null)
            {
                StopCoroutine(_damageCoroutine);
            }

            _damageCoroutine = StartCoroutine(Scan());
        }

        private IEnumerator Scan()
        {
            while (true)
            {
                var result = ScanForEnemy();

                if (result is not null)
                    _hitEnemyEvent.Invoke(result);

                yield return CooldownTime;
            }
        }

        private Collider[] ScanForEnemy()
        {
            return Physics.OverlapSphere(transform.position, Area / 2, _enemyAndWeapon);
        }
    }
}