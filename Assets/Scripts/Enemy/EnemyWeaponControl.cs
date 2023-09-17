using Interface;
using Stats.Instances;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyStatsController))]
    public class EnemyWeaponControl : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private LayerMask _playerLayer;

        private float _damage = 5;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageController))
                {
                    damageController.TakeDamage(_damage);
                }
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageController))
                {
                    damageController.TakeDamage(_damage);
                }
            }
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _damage = newInstance.GetStatByName(Stats.Stats.Amount).Value;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _damage = newInstance.GetStatByName(Stats.Stats.Amount).Value;
        }
    }
}