using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Melee.Garlic
{
    public class GarlicAreaController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private LayerMask _enemyAndWeapon;
        [SerializeField] private UnityEvent<Collider2D[]> _hitEnemyEventHandler;
        [SerializeField] private UnityEvent _startCountdownEventHandler;

        private bool _canDamage;

        private Vector3 _scale;
        private float _defaultArea;
        private float _area;

        private void Awake()
        {
            _scale = transform.localScale;
            _defaultArea = _scale.y;
            _canDamage = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_canDamage) return;

            if (((1 << other.gameObject.layer) & _enemyAndWeapon) != 0)
            {
                _canDamage = false;
                var enemyInside = ScanForEnemy();
                Debug.Log($"enemyInside {enemyInside.Length}");
                _hitEnemyEventHandler.Invoke(enemyInside);
                _startCountdownEventHandler.Invoke();
            }
        }

        public void AllowDamageEventHandler()
        {
            _canDamage = true;
        }

        public void ForbidDamageEventHandler()
        {
            _canDamage = false;
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            SetStat(_defaultArea, weaponInstance.GetStatByName(Stats.Stats.Area).Value);
        }

        private Collider2D[] ScanForEnemy()
        {
            return Physics2D.OverlapCircleAll(transform.position, _scale.y / 2, _enemyAndWeapon);
        }

        private void SetStat(float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            transform.localScale += new Vector3(addValue, addValue, 0);
        }

        // void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawSphere(transform.position, _scale.y);
        // }
    }
}