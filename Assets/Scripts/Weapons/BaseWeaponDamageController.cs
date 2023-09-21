using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons
{
    public class BaseWeaponDamageController : MonoBehaviour, IUpdateStats
    {
        private float _criticalHitChanceDefault;
        private float _criticalHitChance;

        private float _criticalHitMultiplier;

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
            if (TryMakeCriticalHit()) damageable.TakeDamage(_damage * _criticalHitMultiplier);
            else damageable.TakeDamage(_damage);
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            
            _defaultDamage = weaponInstance.GetStatByName(Stats.Stats.Damage).Value;
            _damage = _defaultDamage;
            
            _criticalHitChanceDefault = weaponInstance.GetStatByName(Stats.Stats.Chance).Value;
            _criticalHitChance = _criticalHitChanceDefault;
            
            _criticalHitMultiplier = weaponInstance.GetStatByName(Stats.Stats.CriticalHitMultiplier).Value;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            SetStat(ref _damage, _defaultDamage, newInstance.GetStatByName(Stats.Stats.Damage).Value);
            _criticalHitChance = newInstance.GetStatByName(Stats.Stats.Chance).Value;
        }

        protected virtual void SetStat(ref float variable, float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            variable = defaultValue + addValue;
        }

        private bool TryMakeCriticalHit()
        {
            Debug.Log($"crit hit chance {_criticalHitChance}");
            
            var result = Random.value * 100;
            
            Debug.Log($"roll {result}");
            
            if (result <= _criticalHitChance)
                return true;
            else
                return false;
        }
    }
}