﻿using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatData : ICloneable
    {
        [SerializeField] private Stats _stat;
        [SerializeField] private float _value;

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


        public StatData()
        {
        }

        public StatData(Stats stat, float value)
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