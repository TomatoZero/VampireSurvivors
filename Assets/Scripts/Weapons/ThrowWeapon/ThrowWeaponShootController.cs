using UnityEngine;

namespace Weapons.ThrowWeapon
{
    public abstract class ThrowWeaponShootController : WeaponTopDownShootController
    {
        public virtual Vector2 GetFallPosition(Vector2 mousePosition)
        {
            return transform.InverseTransformPoint(mousePosition);
        }
    }
}