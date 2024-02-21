using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Melee.LightningRing
{
    public class LightningRingEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _particleEffect;

        private Queue<LightningParticleEffectController> _freeParticle;

        private void Awake()
        {
            _freeParticle = new Queue<LightningParticleEffectController>();
        }

        public void SpawnLightning(Vector2[] positions)
        {
            if(positions is null) return;
            
            foreach (var position in positions)
            {
                SpawnLightning(position);
            }
        }
        
        public void SpawnLightning(Vector2 positions)
        {
            if(positions != Vector2.zero)
            {
                var particle = GetParticle();
                particle.Set(positions + new Vector2(0, 3));
            }
        }
        
        private void SetFreeParticle(LightningParticleEffectController gameObject)
        {
            _freeParticle.Enqueue(gameObject);
        }

        private LightningParticleEffectController GetParticle()
        {
            if (_freeParticle.TryDequeue(out LightningParticleEffectController freeParticle))
                return freeParticle;
            else
            {
                var obj = Instantiate(_particleEffect, transform);
                var particleEffect = obj.GetComponent<LightningParticleEffectController>();
                particleEffect.EffectEndEvent += SetFreeParticle;
                return particleEffect;
            }
        }
    }
}