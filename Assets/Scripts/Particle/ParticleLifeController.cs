using System;
using System.Collections;
using Interface;
using Particle.ParticleReferences;
using Stats.Instances;
using UnityEngine;

namespace Particle
{
    public class ParticleLifeController : MonoBehaviour, IUpdateStats
    {
        private WaitForSeconds _particleLifetime;
        private Coroutine _destroyObjectCoroutine;

        private ParticleReference _reference;

        public event Action<ParticleReference> ParticleDieEvent;

        public delegate void DestroyParticle(GameObject particle);

        public event DestroyParticle DestroyParticleEvent;

        private void Awake()
        {
            _particleLifetime = new WaitForSeconds(1);
        }

        public void SetReferenceScript(ParticleReference reference)
        {
            _reference = reference;
        }

        private void OnEnable()
        {
            _destroyObjectCoroutine = StartCoroutine(DestroyObject());
        }

        private void OnDisable()
        {
            StopCoroutine();
        }

        public void DestroyImmediately()
        {
            StopCoroutine();
            gameObject.SetActive(false);
            DestroyParticleEvent?.Invoke(gameObject);

            ParticleDieEvent?.Invoke(_reference);
        }

        private void TryStartCoroutine()
        {
            if (!gameObject.activeSelf) return;
            if (_destroyObjectCoroutine is not null) StopCoroutine();

            _destroyObjectCoroutine = StartCoroutine(DestroyObject());
        }

        private void StopCoroutine()
        {
            if (_destroyObjectCoroutine == null) return;

            StopCoroutine(_destroyObjectCoroutine);
            _destroyObjectCoroutine = null;
        }

        private IEnumerator DestroyObject()
        {
            yield return _particleLifetime;
            gameObject.SetActive(false);
            DestroyParticleEvent?.Invoke(gameObject);

            ParticleDieEvent?.Invoke(_reference);
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
            if (_destroyObjectCoroutine is not null)
                StopCoroutine();

            var lifetime = newInstance.GetStatByName(Stats.Stats.Duration).Value;
            _particleLifetime = new WaitForSeconds(lifetime);
            TryStartCoroutine();
        }
    }
}