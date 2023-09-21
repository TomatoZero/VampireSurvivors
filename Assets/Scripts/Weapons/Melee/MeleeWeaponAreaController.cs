using System;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Melee
{
    public abstract class MeleeWeaponAreaController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private protected LayerMask _enemyAndWeapon;
        [SerializeField] private protected UnityEvent<Collider2D[]> _hitEnemyEventHandler;
        [SerializeField] private protected UnityEvent _startCountdownEventHandler;

        private protected bool CanDamage;

        private protected float DefaultArea;
        private protected float Area;

        protected abstract void OnTriggerStay2D(Collider2D other);

        public void AllowDamageEventHandler()
        {
            CanDamage = true;
        }

        public void ForbidDamageEventHandler()
        {
            CanDamage = false;
        }

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;

            DefaultArea = weaponInstance.GetStatByName(Stats.Stats.Area).Value;
            Area = DefaultArea;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            SetStat(ref Area, DefaultArea, weaponInstance.GetStatByName(Stats.Stats.Area).Value);
        }

        protected virtual void SetStat(ref float variable, float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            variable = defaultValue + addValue;
        }

        protected abstract Collider2D[] ScanForEnemy();
    }
}