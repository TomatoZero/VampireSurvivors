using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatData : ICloneable
    {
        [SerializeField] private Stats _stat;
        [SerializeField] private int _value;

        public Stats Stat
        {
            get => _stat;
            set => _stat = value;
        }

        public int Value
        {
            get => _value;
            set => _value = value;
        }


        public StatData()
        {
        }

        public StatData(Stats stat, int value)
        {
            _stat = stat;
            _value = value;
        }

        public object Clone()
        {
            return new StatData(_stat, _value);
        }
    }
}