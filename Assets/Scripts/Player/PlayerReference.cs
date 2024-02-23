using Controllers;
using UnityEngine;

namespace Player
{
    public class PlayerReference : MonoBehaviour
    {
        [SerializeField] private PlayerLevelController _levelController;
        [SerializeField] private BuffController _buffController;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private DamageController _damageController;

        public PlayerLevelController LevelController => _levelController;
        public BuffController BuffController => _buffController;
        public HealthController HealthController => _healthController;
        public DamageController DamageController => _damageController;
    }
}