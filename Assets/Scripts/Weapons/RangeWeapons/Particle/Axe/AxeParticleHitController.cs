using Stats.Instances;
using UnityEngine;
using Weapons.RangeWeapons.Particle;

namespace Weapons.RangeWeapons.Axe.Particle
{
    public class AxeParticleHitController : ParticleHitController
    {
        private float _area;
        
        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            base.SetupStatEventHandler(newInstance);
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            UpdateLocalScale();
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            base.UpdateStatsEventHandler(newInstance);
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            UpdateLocalScale();
        }
        
        private void UpdateLocalScale()
        {
            var scale = transform.localScale.y;
            transform.localScale -= new Vector3(scale, scale, 0);
            transform.localScale += new Vector3(_area, _area, 0);
        }
    }
}