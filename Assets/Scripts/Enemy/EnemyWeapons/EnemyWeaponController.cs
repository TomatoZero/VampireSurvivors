using System.Collections.Generic;
using Interface;
using Stats.Instances;
using UnityEngine;
using Weapons;
using Weapons.Melee;

namespace Enemy.EnemyWeapons
{
    public class EnemyWeaponController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private List<WeaponReferences> _references;
        [SerializeField] private EnemyReference _enemyReference;
        
        
        private WeaponReferences _currentWeapon;
        
        public WeaponReferences CurrentWeapon => _currentWeapon;
        
        public void ActivateWeapon(EnemyWeaponType weaponType)
        {
            foreach (var reference in _references)
            {
                
            }
        }
        
        public void DeActivateWeapon(EnemyWeaponType weaponType)
        {
            foreach (var reference in _references)
            {
                
            }
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            SentStatsToWeapon(newInstance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            SentStatsToWeapon(newInstance);
        }

        private void SentStatsToWeapon(ObjectInstance newInstance)
        {
            foreach (var reference in _references)
            {
                reference.StatsController.SetupStatEventHandler(newInstance);
                ((MeleeWeaponShootController)(reference.ShootController)).Player =
                    _enemyReference.MovementController.Player;
            }
        }
    }
}