using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponTopDownShootController : MonoBehaviour, IUpdateStats
    {
        public abstract void ShootEventHandler(Vector2 mousePosition);
        public abstract void SetupStatEventHandler(ObjectInstance newInstance);
        public abstract void UpdateStatsEventHandler(ObjectInstance newInstance);
    }
}