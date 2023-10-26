using System;
using Stats.Instances;
using UnityEngine;

namespace Weapons.Melee.Garlic
{
    public class GarlicAreaController : MeleeWeaponAreaController
    {
        private Vector3 _scale;

        private void Awake()
        {
            _scale = transform.localScale;
            CanDamage = true;
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _enemyAndWeapon) == 0) return;
            if (!CanDamage) return;
            
            CanDamage = false;
            var enemyInside = ScanForEnemy();
            _hitEnemyEvent.Invoke(enemyInside);
            _startCountdownEvent.Invoke();
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            base.SetupStatEventHandler(newInstance);
            UpdateLocalScale();
        }

        protected override Collider2D[] ScanForEnemy()
        {
            return Physics2D.OverlapCircleAll(transform.position, transform.localScale.y /2 , _enemyAndWeapon);
        }

        private void UpdateLocalScale()
        {
            var scale = transform.localScale.y;
            transform.localScale -= new Vector3(scale, scale, 0);
            transform.localScale += new Vector3(Area, Area, 0);
        }
    }
}