using System.Collections;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Particle
{
    public class ParticleEnemyDetector : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private protected LayerMask _enemyAndWeapon;
        [SerializeField] private protected UnityEvent<Collider2D[]> _hitEnemyEvent;

        private float _area;

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
                
                if(result is not null)
                    _hitEnemyEvent.Invoke(result);
                
                yield return new WaitForSeconds(.05f);
            }
        }
        
        private Collider2D[] ScanForEnemy()
        {
            return Physics2D.OverlapCircleAll(transform.position, _area /2 , _enemyAndWeapon);
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
        }
    }
}