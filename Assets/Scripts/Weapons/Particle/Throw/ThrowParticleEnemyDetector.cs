using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Particle.Throw
{
    public class ThrowParticleEnemyDetector : ParticleEnemyDetector
    {
        [SerializeField] private UnityEvent<Collider2D> _hitEnemyEvent;
        [SerializeField] private UnityEvent _destroy;
        
        private int _hitTime;
        private int _currentHitTime;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _enemyAndWeapon) == 0) return;

            if (_currentHitTime <= 0)
            {
                _destroy.Invoke();
                return;
            }

            _currentHitTime--;
            _hitEnemyEvent.Invoke(other);
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            base.SetupStatEventHandler(newInstance);
            _hitTime = (int)newInstance.GetStatByName(Stats.Stats.Pierce).Value;

            _currentHitTime = _hitTime;
        }
        
        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            base.UpdateStatsEventHandler(newInstance);
            _hitTime = (int)newInstance.GetStatByName(Stats.Stats.Pierce).Value;

            _currentHitTime = _hitTime;
        }

    }
}