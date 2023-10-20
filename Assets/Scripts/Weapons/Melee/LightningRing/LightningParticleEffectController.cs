using System;
using UnityEngine;

namespace Weapons.Melee.LightningRing
{
    public class LightningParticleEffectController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        public event Action<LightningParticleEffectController> EffectEndEvent;

        private void Awake()
        {
            var main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void Set(Vector2 position)
        {
            transform.position = position;
            TurnOn();
        }

        void OnParticleSystemStopped()
        {
            EffectEndEvent?.Invoke(this);
        }

        private void TurnOn()
        {
            gameObject.SetActive(true);
            _particleSystem.Play();
        }

        private void TurnOff()
        {
            gameObject.SetActive(false);
            _particleSystem.Stop();
        }
    }
}