using Interface;
using Player;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;

namespace Weapons
{
    public class BaseWeaponDamageController : MonoBehaviour, IUpdateStats
    {
        private float _criticalHitChance;
        private float _criticalHitMultiplier;
        private float _damage;

        public virtual void Damage(Collider[] enemy)
        {
            foreach (var oneEnemy in enemy)
            {
                if(oneEnemy is not null)
                {
                    Debug.Log($"oneEnemy {oneEnemy}");
                    Damage(oneEnemy);
                }
            }
        }
        
        public virtual void Damage(Collider2D[] enemy)
        {
            foreach (var oneEnemy in enemy)
            {
                if(oneEnemy is not null)
                {
                    Debug.Log($"oneEnemy {oneEnemy}");
                    Damage(oneEnemy);
                }
            }
        }

        public virtual void Damage(Collider2D enemy)
        {
            if(enemy == null) return;

            if (enemy.gameObject.TryGetComponent(out IDamageable damageController))
                Damage(damageController);
            else if (enemy.gameObject.TryGetComponent(out PlayerReference reference))
                Damage(reference.DamageController);
        }
        
        public virtual void Damage(Collider enemy)
        {
            if(enemy == null) return;

            if (enemy.gameObject.TryGetComponent(out IDamageable damageController))
                Damage(damageController);
            else if (enemy.gameObject.TryGetComponent(out PlayerReference reference))
                Damage(reference.DamageController);
        }

        public virtual void Damage(IDamageable damageable)
        {
            Debug.Log($"_damage {_damage}");
            
            if (TryMakeCriticalHit()) damageable.TakeDamage(_damage * _criticalHitMultiplier);
            else damageable.TakeDamage(_damage);
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
           
            _damage = weaponInstance.GetStatByName(Stats.Stats.Damage).Value;
            _criticalHitChance = weaponInstance.GetStatByName(Stats.Stats.Chance).Value;
            _criticalHitMultiplier = weaponInstance.GetStatByName(Stats.Stats.CriticalHitMultiplier).Value;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            
            _damage = weaponInstance.GetStatByName(Stats.Stats.Damage).Value;
            _criticalHitChance = weaponInstance.GetStatByName(Stats.Stats.Chance).Value;
        }

        private bool TryMakeCriticalHit()
        {
            var result = Random.value * 100;
            return result <= _criticalHitChance;
        }
    }
}