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
        [SerializeField] private UnityEvent<Vector2> _hitPosition;
        [SerializeField] private AmoAmountControl _amoAmountControl;
        
        private float _area;
        private int _baseAmount;
        private float _reloadTime;

        public void ShootTest(Vector2 mousePosition, int numb)
        {
            ShootEventHandler(mousePosition);
        }
        
        public override void ShootEventHandler(Vector2 mousePosition)
        {
            if (_amoAmountControl.IsEnoughAmo)
            {
                _amoAmountControl.TakeAmo();

                var enemies = ScanForEnemyInCircle(mousePosition);
                
                _hitPosition.Invoke(mousePosition);
                _hitEnemy.Invoke(enemies);
            }
        }
        
        private Collider2D[] ScanForEnemyInCircle(Vector2 position)
        {
            return Physics2D.OverlapCircleAll(position, _area / 2, _enemyLayer) ?? Array.Empty<Collider2D>();
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _reloadTime = newInstance.GetStatByName(Stats.Stats.Reload).Value;
            
            _amoAmountControl.SetAmoData(_baseAmount, _reloadTime);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _reloadTime = newInstance.GetStatByName(Stats.Stats.Reload).Value;
            
            _amoAmountControl.SetAmoData(_baseAmount, _reloadTime);
        }
    }
}