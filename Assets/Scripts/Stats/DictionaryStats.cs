using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private Stats _stat;
        [SerializeField] private int _value;
    }
}