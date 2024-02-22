using UnityEngine;

namespace Enemy
{
    public class EnemyReference : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private EnemyHealthController _healthController;

        public EnemyMovementController MovementController => _movementController;
        public EnemyHealthController HealthController => _healthController;
    }
}