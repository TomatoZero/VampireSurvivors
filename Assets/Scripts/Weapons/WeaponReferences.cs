using Stats.Instances.PowerUp;
using UnityEngine;

namespace Weapons
{
    public class WeaponReferences : MonoBehaviour
    {
        [SerializeField] private WeaponStatsController _statsController;
        [SerializeField] private WeaponTopDownShootController _shootController;
        [SerializeField] private AmoAmountControl _amountControl;
        
        public WeaponStatsController StatsController => _statsController;
        public WeaponTopDownShootController ShootController => _shootController;
        public WeaponInstance Instance => _statsController.Instance;
        public AmoAmountControl AmountControl => _amountControl;
    }
}