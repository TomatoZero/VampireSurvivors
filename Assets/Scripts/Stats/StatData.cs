using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatData : ICloneable
    {
        [SerializeField] private Stats _stat;
        [SerializeField] private float _value;
        [SerializeField] private bool _isPercent;

        public Stats Stat
        {
            get => _stat;
            set => _stat = value;
        }

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public bool IsPercent
        {
            get => _isPercent;
            set => _isPercent = value;
        }

        public StatData()
        {
        }

        public StatData(Stats stat, float value, bool isPercent)
        {
            _stat = stat;
            _value = value;
            _isPercent = isPercent;
        }

        public object Clone()
        {
            return new StatData(_stat, _value, _isPercent);
        }

        public override string ToString()
        {
            return $"stat {_stat} value {_value} is percent {_isPercent}";
        }
    }
}