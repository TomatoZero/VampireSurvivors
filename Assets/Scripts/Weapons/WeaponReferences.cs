using UnityEngine;

namespace Weapons
{
    public class WeaponReferences : MonoBehaviour
    {
        [SerializeField] private WeaponStatsController _statsController;

        public WeaponStatsController StatsController => _statsController;
    }
}