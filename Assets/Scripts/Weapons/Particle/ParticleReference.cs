using System;
using UnityEngine;
using Weapons.RangeWeapons.Particle;

namespace Weapons.Particle
{
    public class ParticleReference : MonoBehaviour
    {
        [SerializeField] private ParticleStatsController _statsController;
        [SerializeField] private ParticleLifeController _particleLifeController;

        public ParticleStatsController StatsController => _statsController;
        public ParticleLifeController ParticleLifeController => _particleLifeController;

        private void Start()
        {
            _particleLifeController.SetReferenceScript(this);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}