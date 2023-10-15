using System;
using Stats;
using Stats.Instances;
using Stats.Instances.PowerUp;
using UnityEngine;

namespace DefaultNamespace.UI.CurrentStatsMenu
{
    public class CurrentStatsDisplay : MonoBehaviour
    {
        [SerializeField, Header("Player Stats")]
        private StatDisplay _maxHealth;

        [SerializeField] private StatDisplay _recovery;
        [SerializeField] private StatDisplay _armor;
        [SerializeField] private StatDisplay _moveSpeed;

        [SerializeField, Space(5), Header("Weapon stats")]
        private StatDisplay _might;

        [SerializeField] private StatDisplay _area;
        [SerializeField] private StatDisplay _speed;
        [SerializeField] private StatDisplay _duration;
        [SerializeField] private StatDisplay _amount;
        [SerializeField] private StatDisplay _cooldown;

        [SerializeField, Space(5), Header("Additional stats")]
        private StatDisplay _luck;

        [SerializeField] private StatDisplay _growth;
        [SerializeField] private StatDisplay _greed;
        [SerializeField] private StatDisplay _revival;

        // private void OnEnable()
        // {
        //     _maxHealth.Setup("Max health", "0");
        //     _recovery.Setup("Recovery", "0");
        //     _armor.Setup("Armor", "0");
        //     _moveSpeed.Setup("Move speed", "0");
        //     _might.Setup("Damage", "+0");
        //     _area.Setup("Area", "+0");
        //     _speed.Setup("Speed", "+0");
        //     _duration.Setup("Duration", "+0");
        //     _amount.Setup("Amount", "+0");
        //     _cooldown.Setup("Countdown", "+0");
        //     _luck.Setup("Luck","+0");
        //     _growth.Setup("Growth", "+0");
        //     _greed.Setup("Greed", "+0");
        //     _revival.Setup("Revival", "0");
        // }

        public void SetCurrentStats(PlayerInstance playerInstance)
        {
            foreach (var stat in playerInstance.CurrentStats)
            {
                SetStat(stat.Stat, stat.Value, stat.IsPercent);
            }

            foreach (var stat in playerInstance.PlayerStatCalculator.ClearBonuses)
            {
                SetBonus(stat.Key, stat.Value, false);
            }
            
            foreach (var stat in playerInstance.PlayerStatCalculator.PercentBonuses)
            {
                SetBonus(stat.Key, stat.Value, true);
            }
        }

        public void ShowCurrentStats()
        {
            gameObject.SetActive(true);
        }

        public void HideCurrentStats()
        {
            gameObject.SetActive(false);
        }
        
        private void SetStat(Stats.Stats stat, float value, bool isPercent)
        {
            switch (stat)
            {
                case Stats.Stats.MaxHealth:
                    _maxHealth.Setup("Max health", $"{value}");
                    break;
                case Stats.Stats.MoveSpeed:
                    _moveSpeed.Setup("Move speed", $"+{value}%");
                    break;
                case Stats.Stats.Recovery:
                    _recovery.Setup("Recovery", $"+{stat}");
                    break;
                case Stats.Stats.Armor:
                    _armor.Setup("Armor", $"+{stat}");
                    break;
                case Stats.Stats.Luck:
                    _luck.Setup("Luck",$"+{stat}%");
                    break;
                case Stats.Stats.Growth:
                    _growth.Setup("Growth", $"+{stat}%");
                    break;
                case Stats.Stats.Greed:
                    _greed.Setup("Greed", $"+{stat}%");
                    break;
            }
        }

        private void SetBonus(Stats.Stats stat, float value, bool isPercent)
        {
            switch (stat)
            {
                case Stats.Stats.Damage:
                    _might.Setup("Damage", $"+{value}%");
                    break;
                case Stats.Stats.Area:
                    _area.Setup("Area", $"+{value}%");
                    break;
                case Stats.Stats.ProjectilesSpeed:
                    _speed.Setup("Speed", $"+{value}%");
                    break;
                case Stats.Stats.Duration:
                    _duration.Setup("Duration", $"+{value}%");
                    break;
                case Stats.Stats.Amount:
                    _amount.Setup("Amount", $"+{value}");
                    break;
                case Stats.Stats.Cooldown:
                    _cooldown.Setup("Countdown", $"+{value}%");
                    break;
                case Stats.Stats.Revival:
                    _revival.Setup("Revival", $"+{value}");
                    break;
            }
        }
    }
}