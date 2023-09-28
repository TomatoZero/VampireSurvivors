using System;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.RangeWeapons.LightningRing
{
    public class LightningRingShootController : RangeWeaponShootController
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private UnityEvent<Collider2D[]> _hitEnemy;

        private readonly Vector2 _boxSize = new Vector2(35, 20);

        private Collider2D[] _enemyHit;

        private protected override void Awake()
        {
            _enemyHit = Array.Empty<Collider2D>();
            base.Awake();
        }

        public override void Shoot()
        {
            var allEnemyAtScreen = ScanForEnemy();
            
            var step = allEnemyAtScreen.Length / Amount;

            if (step is 0)
            {
                StartTimerEvent.Invoke();
                return;
            }
            
            for (int i = 0, j = 0; i < allEnemyAtScreen.Length - 1 || j < Amount ; i += step, j++)
            {
                _enemyHit[j] = allEnemyAtScreen[i];
            }

            _hitEnemy.Invoke(_enemyHit);
            StartTimerEvent.Invoke();
        }

        protected Collider2D[] ScanForEnemy()
        {
            var output = Physics2D.OverlapBoxAll(transform.position, _boxSize, 0, _layerMask);
            return output ?? Array.Empty<Collider2D>();
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            base.SetupStatEventHandler(newInstance);
            _enemyHit = new Collider2D[Amount];
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            base.UpdateStatsEventHandler(newInstance);
            _enemyHit = new Collider2D[Amount];
        }
    }
}