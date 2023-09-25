using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.RangeWeapons.Particle
{
    public abstract class ParticleMoveController : MonoBehaviour, IUpdateStats
    {
        private float _speed;
        private Vector2 _moveDirection;

        private protected abstract void FixedUpdate();
        public abstract Vector2 Move();
        private protected abstract Vector2 GetRandomMoveDirection();


        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var weapon = (WeaponInstance)newInstance;

            _speed = weapon.GetStatByName(Stats.Stats.ProjectilesSpeed).Value;
            Move();
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {}
        
        
    }
}