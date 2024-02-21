using System;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.ThrowWeapon
{
    public class LightningRingShootController : ThrowWeaponShootController
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private UnityEvent<Collider2D[]> _hitEnemy;
        
        private float _area;

        public void ShootTest(Vector2 mousePosition, int numb)
        {
            ShootEventHandler(mousePosition);
        }
        
        public override void ShootEventHandler(Vector2 mousePosition)
        {
            if (AmoAmountControl.IsEnoughAmo)
            {
                AmoAmountControl.TakeAmo();

                var enemies = ScanForEnemyInCircle(mousePosition);
                
                HitPosition.Invoke(mousePosition);
                _hitEnemy.Invoke(enemies);
            }
        }
        
        private Collider2D[] ScanForEnemyInCircle(Vector2 position)
        {
            return Physics2D.OverlapCircleAll(position, _area / 2, _enemyLayer) ?? Array.Empty<Collider2D>();
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            base.UpdateStatsEventHandler(newInstance);
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            
            AmoAmountControl.SetAmoData(BaseAmount, ReloadTime);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            base.UpdateStatsEventHandler(newInstance);
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            
            AmoAmountControl.SetAmoData(BaseAmount, ReloadTime);
        }
    }
}