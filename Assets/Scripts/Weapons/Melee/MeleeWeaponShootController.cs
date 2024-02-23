using System;
using System.Linq;
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

        public override void ShootEventHandler(Vector2 mousePosition)
        {
            // if (_weapon.AmountControl.IsEnoughAmo)
            if (true)
            {
                var direction = (mousePosition - (Vector2)transform.position).normalized;

                _cubePos = direction;

                var result = ScanForEnemy(direction);
                _hitEnemy.Invoke(result);
                
                // Debug.Log($"result {result.Length}");
            }
        }

        private Collider2D[] ScanForEnemy(Vector2 position)
        {
            // return Physics2D.OverlapCircleAll(transform.position, _area);
            return Physics2D.OverlapBoxAll(position, _cubeSize, _enemyLayer) ?? Array.Empty<Collider2D>();
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _baseReload = newInstance.GetStatByName(Stats.Stats.Reload).Value;

            _weapon.AmountControl.SetAmoData(_baseAmount, _baseReload);
            
            _cubeSize = new Vector3(_area , _area, _area);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _baseReload = newInstance.GetStatByName(Stats.Stats.Reload).Value;

            _weapon.AmountControl.SetAmoData(_baseAmount, _baseReload);

            _cubeSize = new Vector3(_area / 2, _area / 2, _area / 2);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            
            Gizmos.DrawRay(transform.position, _cubePos * _area);
            Gizmos.DrawCube(_cubePos, _cubeSize * 2);
        }
    }
}