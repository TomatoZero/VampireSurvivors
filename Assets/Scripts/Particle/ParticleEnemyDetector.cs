using Interface;
using Stats.Instances;
using UnityEngine;

namespace Particle
{
    public abstract class ParticleEnemyDetector : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private protected LayerMask _enemyAndWeapon;

        private float _area;
        private float _cooldown;

        private WaitForSeconds _cooldownTime;

        protected float Area => _area;
        protected WaitForSeconds CooldownTime => _cooldownTime;

        public virtual void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _cooldown = newInstance.GetStatByName(Stats.Stats.Cooldown).Value;
            _cooldownTime = new WaitForSeconds(_cooldown);

            UpdateLocalScale();
        }

        public virtual void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            _cooldown = newInstance.GetStatByName(Stats.Stats.Cooldown).Value;
            _cooldownTime = new WaitForSeconds(_cooldown);

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