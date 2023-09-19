using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons
{
    public abstract class BaseWeaponDamageController : MonoBehaviour, IUpdateStats
    {
        private float _defaultDamage;
        private float _damage;

        public virtual void Damage(Collider2D[] enemy)
        {
            foreach (var oneEnemy in enemy) Damage(oneEnemy);
        }

        public virtual void Damage(Collider2D enemy)
        {
            if (enemy.gameObject.TryGetComponent(out IDamageable damageController))
                Damage(damageController);
        }

        public virtual void Damage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            _defaultDamage = weaponInstance.GetStatByName(Stats.Stats.Damage).Value;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            SetStat(ref _damage, _defaultDamage, newInstance.GetStatByName(Stats.Stats.Damage).Value);
        }

        protected virtual void SetStat(ref float variable, float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            variable = defaultValue + addValue;
        }
    }
}