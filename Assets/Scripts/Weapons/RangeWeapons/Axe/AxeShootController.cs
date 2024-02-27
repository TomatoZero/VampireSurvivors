using System.Collections;
using Stats.Instances;
using UnityEngine;

namespace Weapons.RangeWeapons.Axe
{
    public class AxeShootController : RangeWeaponShootController
    {
        private WaitForSeconds _projectileInterval;
        
        private protected override void Awake()
        {
            _projectileInterval = new WaitForSeconds(.2f);
        }

        public override void Shoot()
        {
            StartCoroutine(ShootWithDelay());
        }

        private IEnumerator ShootWithDelay()
        {
            for (int i = 0; i < Amount; i++)
            {
                CreateInstance();
                yield return _projectileInterval;
            }
            StartTimerEvent.Invoke();
        }
        
        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            var interval = newInstance.GetStatByName(Stats.Stats.ProjectilesInterval).Value;
            _projectileInterval = new WaitForSeconds(interval);
            base.SetupStatEventHandler(newInstance);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var interval = newInstance.GetStatByName(Stats.Stats.ProjectilesInterval).Value;
            _projectileInterval = new WaitForSeconds(interval);
            base.UpdateStatsEventHandler(newInstance);
        }
    }
}