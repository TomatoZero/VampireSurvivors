using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Weapons.RangeWeapons.Particle
{
    public class ParticleHitController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private UnityEvent<Collider2D> _hitEnemyEvent;
        [SerializeField] private UnityEvent _destroyParticleEvent;
        [SerializeField] private LayerMask _enemyAndWeapon;

        private int _maxPierce;
        private int _currentPierce;
            
        private protected UnityEvent<Collider2D> HitEnemyEventHandler
        {
            get => _hitEnemyEvent;
            set => _hitEnemyEvent = value;
        }
        private protected UnityEvent DestroyParticleEventHandler
        {
            get => _destroyParticleEvent;
            set => _destroyParticleEvent = value;
        }
        private protected LayerMask EnemyAndWeapon
        {
            get => _enemyAndWeapon;
            set => _enemyAndWeapon = value;
        }
        private protected int Pierce
        {
            get => _maxPierce;
            set => _maxPierce = value;
        }

        private protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _enemyAndWeapon) != 0)
            {
                if (_currentPierce < _maxPierce)
                {
                    _hitEnemyEvent.Invoke(other);
                    _currentPierce++;
                }
                else
                {
                    _destroyParticleEvent.Invoke();
                }
            }
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _maxPierce = (int)newInstance.GetStatByName(Stats.Stats.Pierce).Value;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _maxPierce = (int)newInstance.GetStatByName(Stats.Stats.Pierce).Value;
        }
    }
}