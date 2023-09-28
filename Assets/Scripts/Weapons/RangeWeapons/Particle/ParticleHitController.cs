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

        private float _pierce;

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
        private protected float Pierce
        {
            get => _pierce;
            set => _pierce = value;
        }

        private protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _enemyAndWeapon) != 0)
            {
                if (_pierce > 0)
                    _hitEnemyEvent.Invoke(other);
                else
                {
                    _destroyParticleEvent.Invoke();
                }
            }
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _pierce = newInstance.GetStatByName(Stats.Stats.Pierce).Value;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _pierce = newInstance.GetStatByName(Stats.Stats.Pierce).Value;
        }
    }
}