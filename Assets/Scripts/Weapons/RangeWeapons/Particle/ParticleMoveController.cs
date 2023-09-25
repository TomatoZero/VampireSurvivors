using System;
using UnityEngine;

namespace Weapons.RangeWeapons.Particle
{
    public abstract class ParticleMoveController : MonoBehaviour
    {
        private Vector2 _moveDirection;

        private protected abstract void FixedUpdate();

        public abstract Vector2 MoveTo(Vector2 direction);
    }
}