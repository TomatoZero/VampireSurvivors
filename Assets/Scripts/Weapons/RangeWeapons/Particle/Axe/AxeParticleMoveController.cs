using UnityEngine;
using Weapons.RangeWeapons.Particle;

namespace Weapons.RangeWeapons.Axe.Particle
{
    public class AxeParticleMoveController : ParticleMoveController
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private protected override void FixedUpdate()
        {
        }

        public override Vector2 Move()
        {
            var direction = GetRandomMoveDirection();
            AddRotation(direction);
            _rigidbody.AddForce(direction * 10, ForceMode2D.Impulse);
            return Vector2.zero;
        }

        private protected override Vector2 GetRandomMoveDirection()
        {
            var x = Random.Range(-0.5f, 0.5f);
            var y = Random.value + 0.5f;
            return new Vector2(x, y);
        }

        private void AddRotation(Vector2 pushDirection)
        {
            _rigidbody.AddTorque(pushDirection.x * -15);
        }
    }
}