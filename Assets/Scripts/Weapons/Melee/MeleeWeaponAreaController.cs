using System;
using Interface;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Weapons.Melee
{
    public abstract class MeleeWeaponAreaController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private protected LayerMask _enemyAndWeapon;
        [FormerlySerializedAs("_hitEnemyEventHandler")] [SerializeField] private protected UnityEvent<Collider2D[]> _hitEnemyEvent;
        [FormerlySerializedAs("_startCountdownEventHandler")] [SerializeField] private protected UnityEvent _startCountdownEvent;

        private protected bool CanDamage;
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
            Area = weaponInstance.GetStatByName(Stats.Stats.Area).Value;
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            SetupStatEventHandler(newInstance);
        }


        protected abstract Collider2D[] ScanForEnemy();
    }
}