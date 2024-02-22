using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Enemy.EnemyWeapons
{
    public class EnemyWeaponController : MonoBehaviour
    {
        [SerializeField] private List<WeaponReferences> _references;

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
    }
}