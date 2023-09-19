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
            DefaultArea = _scale.y;
            CanDamage = true;
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            if (!CanDamage) return;

            if (((1 << other.gameObject.layer) & _enemyAndWeapon) != 0)
            {
                CanDamage = false;
                var enemyInside = ScanForEnemy();
                Debug.Log($"enemyInside {enemyInside.Length}");
                _hitEnemyEventHandler.Invoke(enemyInside);
                _startCountdownEventHandler.Invoke();
            }
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            base.UpdateStatsEventHandler(newInstance);
            transform.localScale += new Vector3(Area - DefaultArea, Area - DefaultArea, 0);
        }

        protected override Collider2D[] ScanForEnemy()
        {
            return Physics2D.OverlapCircleAll(transform.position, _scale.y / 2, _enemyAndWeapon);
        }
    }
}