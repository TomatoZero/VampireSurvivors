using Interface;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;

namespace Weapons.Melee.Garlic
{
    [RequireComponent(typeof(Collider2D))]
    public class GarlicDamageController : MonoBehaviour, IWeaponDamageController, IUpdateStats
    {
        private float _defaultDamage;
        private float _damage;

        public void Damage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
        }

        public void Damage(Collider2D[] enemy)
        {
            foreach (var oneEnemy in enemy)
            {
                if (oneEnemy.gameObject.TryGetComponent(out IDamageable damageController)) Damage(damageController);
            }
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            _defaultDamage = weaponInstance.GetStatByName(Stats.Stats.Damage).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            SetStat(ref _damage, _defaultDamage, newInstance.GetStatByName(Stats.Stats.Damage).Value);
        }

        private void SetStat(ref float variable, float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            variable = defaultValue + addValue;
        }
    }
}