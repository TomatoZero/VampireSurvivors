using System;
using Stats;
using Stats.Instances;
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

        private void Awake()
        {
            _maxHealth.Setup("Max health", "0");
            _recovery.Setup("Recovery", "0");
            _armor.Setup("Armor", "0");
            _moveSpeed.Setup("Move speed", "0");
            _might.Setup("Damage", "0");
            _area.Setup("Area", "0");
            _speed.Setup("Speed", "0");
            _duration.Setup("Duration", "0");
            _amount.Setup("Amount", "0");
            _cooldown.Setup("Countdown", "0");
            _luck.Setup("Luck","0");
            _growth.Setup("Growth", "0");
            _greed.Setup("Greed", "0");
            _revival.Setup("Revival", "0");
        }

        public void SetCurrentStats(PlayerInstance playerInstance)
        {
            var s = "";
            foreach (var stat in playerInstance.CurrentStats)
            {
                SetStat(stat);
                s += stat + "\n";
            }
            
            Debug.Log(s);
        }

        public void ShowCurrentStats()
        {
            gameObject.SetActive(true);
        }

        public void HideCurrentStats()
        {
            gameObject.SetActive(false);
        }
        
        private void SetStat(StatData statData)
        {
            switch (statData.Stat)
            {
                case Stats.Stats.MaxHealth:
                    _maxHealth.Setup("Max health", $"{statData.Value}");
                    break;
                case Stats.Stats.Recovery:
                    _recovery.Setup("Recovery", $"+{statData.Value}");
                    break;
                case Stats.Stats.Armor:
                    _armor.Setup("Armor", $"+{statData.Value}");
                    break;
                case Stats.Stats.MoveSpeed:
                    _moveSpeed.Setup("Move speed", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Damage:
                    _might.Setup("Damage", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Area:
                    _area.Setup("Area", $"+{statData.Value}%");
                    break;
                case Stats.Stats.ProjectilesSpeed:
                    _speed.Setup("Speed", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Duration:
                    _duration.Setup("Duration", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Amount:
                    _amount.Setup("Amount", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Cooldown:
                    _cooldown.Setup("Countdown", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Luck:
                    _luck.Setup("Luck",$"+{statData.Value}%");
                    break;
                case Stats.Stats.Growth:
                    _growth.Setup("Growth", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Greed:
                    _greed.Setup("Greed", $"+{statData.Value}%");
                    break;
                case Stats.Stats.Revival:
                    _revival.Setup("Revival", $"+{statData.Value}");
                    break;
            }
        }
    }
}