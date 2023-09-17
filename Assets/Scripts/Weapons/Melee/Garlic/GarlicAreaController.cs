using System;
using Interface;
using Stats.Instances;
using UnityEngine;

namespace Weapons.Melee.Garlic
{
    public class GarlicAreaController : MonoBehaviour, IUpdateStats
    {
        private Vector3 _scale;
        private float _defaultArea;
        private float _area;

        private void Awake()
        {
            _scale = transform.localScale;
            _defaultArea = _scale.y;
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            var weaponInstance = (WeaponInstance)newInstance;
            SetStat(_defaultArea, weaponInstance.GetStatByName(Stats.Stats.Area).Value);
        }
        
        private void SetStat(float defaultValue, float addPercent)
        {
            var addValue = (defaultValue * addPercent) / 100;
            _area = _defaultArea + addValue;
            
            _scale.Set(_area, _area, 1);
        }
    }
}