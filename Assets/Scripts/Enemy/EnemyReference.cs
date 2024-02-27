using Controllers;
using Enemy.EnemyWeapons;
using UnityEngine;

namespace Enemy
{
    public class EnemyReference : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private EnemyHealthController _healthController;
        [SerializeField] private EnemyWeaponController _weaponController;
        [SerializeField] private BuffController _buffController;

        public EnemyMovementController MovementController => _movementController;
        public EnemyHealthController HealthController => _healthController;
        public EnemyWeaponController WeaponController => _weaponController;
        public BuffController BuffController => _buffController;
    }
}