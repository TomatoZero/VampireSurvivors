using UnityEngine;

namespace Weapons
{
    public class WeaponReferences : MonoBehaviour
    {
        [SerializeField] private WeaponStatsController _statsController;
        [SerializeField] private WeaponTopDownShootController _shootController;
        
        public WeaponStatsController StatsController => _statsController;
        public WeaponTopDownShootController ShootController => _shootController;
    }
}