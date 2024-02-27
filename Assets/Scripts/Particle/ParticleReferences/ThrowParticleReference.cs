using Particle.Throw.Move;
using UnityEngine;

namespace Particle.ParticleReferences
{
    public class ThrowParticleReference : ParticleReference
    {
        [SerializeField] private ParticleMoveController _moveController;

        public ParticleMoveController MoveController => _moveController;
    }
}