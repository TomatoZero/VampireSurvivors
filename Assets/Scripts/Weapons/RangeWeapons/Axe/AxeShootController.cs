using UnityEngine;

namespace Weapons.RangeWeapons.Axe
{
    public class AxeShootController : RangeWeaponShootController
    {
        public override void Shoot()
        {
            for (int i = 0; i < Amount; i++)
            {
                var moveDirection = GetRandomShootDirection();
                CreateInstance(moveDirection);
            }
        }

        private protected override Vector2 GetRandomShootDirection()
        {
            var value = (int)Random.value;
            return new Vector2(value, 0);
        }
    }
}