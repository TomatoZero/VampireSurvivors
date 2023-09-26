using System;
using System.Collections;
using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.RangeWeapons.Particle
{
    public class ParticleLifeController : MonoBehaviour, IUpdateStats
    {
        private WaitForSeconds _particleLifetime;

        public delegate void DestroyParticle(GameObject particle);
        public event DestroyParticle DestroyParticleEvent;
        
        private void Awake()
        {
            _particleLifetime = new WaitForSeconds(1);
        }

        private IEnumerator DestroyObject()
        {
            yield return _particleLifetime;
            gameObject.SetActive(false);
            DestroyParticleEvent.Invoke(gameObject);
        }

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
            StopCoroutine(DestroyObject());
            var lifetime = newInstance.GetStatByName(Stats.Stats.Duration).Value;
            _particleLifetime = new WaitForSeconds(lifetime);
            StartCoroutine(DestroyObject());
        }
    }
}