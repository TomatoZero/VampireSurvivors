using System.Collections.Generic;
using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelWeaponsAndItems", menuName = "ScriptableObject/LevelWeaponsAndItems", order = 0)]
    public class LevelWeaponsAndItems : ScriptableObject
    {
        [SerializeField] private List<WeaponStatsData> _weapons;
        [SerializeField] private List<ObjectStatsData> _items;

        public List<WeaponStatsData> Weapons => _weapons;

        public List<ObjectStatsData> Items => _items;
    }
}