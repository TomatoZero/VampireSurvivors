using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.RangeWeapons.Particle
{
    public abstract class
        ParticleMoveController : MonoBehaviour, IUpdateStats
        //TODO: this class violate SOLID principe #3 Liskov substitution principle.
        //You should remove this class and create few interface for movement
        //For example IFly and IFall after that create few scripts for different type of movement
        //Like FallParticleMove and FlyParticleMove this is base classes 
    {
        private float _speed;
        private Vector2 _moveDirection;

        private protected abstract void FixedUpdate();
        public abstract Vector2 Move();
        private protected abstract Vector2 GetRandomMoveDirection();


        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            GetStatFromInstance(newInstance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            GetStatFromInstance(newInstance);
        }

        private void GetStatFromInstance(ObjectInstance newInstance)
        {
            _speed = newInstance.GetStatByName(Stats.Stats.ProjectilesSpeed).Value;
            Move();
        }
    }
}