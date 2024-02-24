using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.Particle
{
    public class ParticleVisualController : MonoBehaviour, IUpdateStats
    {
        private float _area;
        
        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            UpdateLocalScale();
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            UpdateLocalScale();
        }
        
        private void UpdateLocalScale()
        {
            var scale = transform.localScale.y;
            transform.localScale -= new Vector3(scale, scale, scale);
            transform.localScale += new Vector3(_area, _area, _area);
        }
    }
}