using Interface;
using Stats.Instances;
using UnityEngine;

namespace Enemy
{
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

        public void UpdateStatsEventHandler(PlayerStatsInstance newStatsInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}