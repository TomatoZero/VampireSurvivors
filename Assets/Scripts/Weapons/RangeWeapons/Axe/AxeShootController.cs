using UnityEngine;

namespace Weapons.RangeWeapons.Axe
{
    public class AxeShootController : RangeWeaponShootController
    {
        public override void Shoot()
        {
            for (int i = 0; i < Amount; i++)
            {
                CreateInstance();
            }
        }
    }
}