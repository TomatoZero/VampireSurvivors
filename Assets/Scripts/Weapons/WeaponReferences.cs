using Stats.Instances.PowerUp;
using UnityEngine;
using Weapons.StatsController;

namespace Weapons
{
    public class WeaponReferences : MonoBehaviour
    {
        [SerializeField] private BaseWeaponStatController _statsController;
        [SerializeField] private WeaponTopDownShootController _shootController;
        [SerializeField] private AmoAmountControl _amountControl;
        
        public BaseWeaponStatController StatsController => _statsController;
        public WeaponTopDownShootController ShootController => _shootController;
        public WeaponInstance Instance => _statsController.Instance;
        public AmoAmountControl AmountControl => _amountControl;
    }
}