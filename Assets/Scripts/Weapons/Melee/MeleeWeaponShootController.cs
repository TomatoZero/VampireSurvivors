using System;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Melee
{
    public class MeleeWeaponShootController : WeaponTopDownShootController
    {
        [SerializeField] private WeaponReferences _weapon;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private UnityEvent<Collider2D[]> _hitEnemy;

        [SerializeField] private Transform _player;

        private float _area;
        private int _baseAmount;
        private float _baseReload;

        private Vector2 _cubePos;
        private Vector3 _cubeSize;
        
        private void Update()
        {
            ShootEventHandler(_player.position);
        }

        public override void ShootEventHandler(Vector3 mousePosition)
        {
            if (_weapon.AmountControl.IsEnoughAmo)
            {
                mousePosition = transform.InverseTransformDirection(mousePosition);
                // Debug.Log($"mousePosition {mousePosition}");

                
                var direction = (mousePosition - transform.position).normalized;

                _cubePos = direction;

                var result = ScanForEnemy(direction.normalized);

                Debug.DrawRay(transform.position, direction);
                
                if (result is not null)
                {
                    _weapon.AmountControl.TakeAmo();   
                    _hitEnemy.Invoke(result);
                }
            }
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _baseReload = newInstance.GetStatByName(Stats.Stats.Reload).Value;

            _weapon.AmountControl.SetAmoData(_baseAmount, _baseReload);

            _cubeSize = new Vector3(_area, _area, _area);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _baseReload = newInstance.GetStatByName(Stats.Stats.Reload).Value;

            _weapon.AmountControl.SetAmoData(_baseAmount, _baseReload);

            _cubeSize = new Vector3(_area, _area, _area);
        }

        private Collider2D[] ScanForEnemy(Vector2 position)
        {
            return Physics2D.OverlapBoxAll(position, _cubeSize, _enemyLayer) ?? Array.Empty<Collider2D>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawRay(transform.position, _cubePos);
            Gizmos.DrawCube(_cubePos, _cubeSize);
        }
    }
}