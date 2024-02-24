using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player
{
    public class WeaponsShootController : MonoBehaviour
    {
        private List<WeaponReferences> _weaponReferences;

        private void Awake()
        {
            _weaponReferences = new List<WeaponReferences>();
        }

        public void AddWeapon(WeaponReferences weapon)
        {
            if (weapon is null) return;

            _weaponReferences.Add(weapon);
        }

        public void Shoot(Vector3 mousePos, int weaponNumber)
        {
            if (weaponNumber - 1 < 0 || weaponNumber - 1 >= _weaponReferences.Count) return;
           
            _weaponReferences[weaponNumber - 1].ShootController.ShootEventHandler(mousePos);
        }
    }
}