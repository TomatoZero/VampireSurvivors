using System;
using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.Melee.Garlic
{
    [RequireComponent(typeof(Collider2D))]
    public class GarlicDamageController : MonoBehaviour, IWeaponDamageController, IUpdateStats
    {
        [SerializeField] private LayerMask _enemyAndWeapon;

        private bool _canDamage;
        private float _defaultDamage;
        private float _damage;

        private void Awake()
        {
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_canDamage) return;

            if (((1 << other.gameObject.layer) & _enemyAndWeapon) != 0)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageController)) Damage(damageController);
            }
        }

        public void Damage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            SetStat(ref _damage, _defaultDamage, newInstance.GetStatByName(Stats.Stats.Damage).Value);
        }

        public void AllowDamageEventHandler()
        {
            _canDamage = true;
        }

        public void ForbidDamageEventHandler()
        {
            _canDamage = false;
        }

        private void SetStat(ref float variable, float defaultValue, float addPercent)
        {
            if (variable <= 0) throw new ArgumentOutOfRangeException(nameof(variable));

            var addValue = (defaultValue * addPercent) / 100;
            variable = defaultValue + addValue;
        }
    }
}