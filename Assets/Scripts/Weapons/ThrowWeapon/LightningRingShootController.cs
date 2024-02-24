using System;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.ThrowWeapon
{
    public class LightningRingShootController : ThrowWeaponShootController
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private UnityEvent<Collider[]> _hitEnemy;
        
        private float _area;

        public void ShootTest(Vector2 mousePosition, int numb)
        {
            ShootEventHandler(mousePosition);
        }
        
        public override void ShootEventHandler(Vector3 mousePosition)
        {
            if (AmoAmountControl.IsEnoughAmo)
            {
                AmoAmountControl.TakeAmo();

                var enemies = ScanForEnemyInCircle(mousePosition);
                
                HitPosition.Invoke(mousePosition);
                _hitEnemy.Invoke(enemies);
            }
        }
        
        private Collider[] ScanForEnemyInCircle(Vector2 position)
        {
            return Physics.OverlapSphere(position, _area / 2, _enemyLayer) ?? Array.Empty<Collider>();
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