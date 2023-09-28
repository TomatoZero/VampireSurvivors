using System;
using Interface;
using Stats.Instances;
using UnityEngine;
using Weapons;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private WeaponStatsController[] _weapons;

        private delegate void StatsUpdate(PlayerInstance instance);

        private event StatsUpdate SetupStatEvent;
        private event StatsUpdate UpdateStatEvent;

        private void OnEnable()
        {
            foreach (var weapon in _weapons)
            {
                SetupStatEvent += weapon.SetupStatEventHandler;
                UpdateStatEvent += weapon.UpdateStatsEventHandler;
            }
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            SetupStatEvent?.Invoke((PlayerInstance)newInstance);
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            UpdateStatEvent?.Invoke((PlayerInstance)newInstance);
        }
    }
}