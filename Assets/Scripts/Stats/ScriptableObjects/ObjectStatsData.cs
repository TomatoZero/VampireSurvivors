using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ObjectStatsData", menuName = "ScriptableObject/Stats/ObjectStatsData", order = 3)]
    public class ObjectStatsData : ScriptableObject
    {
        [SerializeField] private Sprite _ico;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private List<StatData> _defaultStatsData;
        [SerializeField] private List<LevelUpBonuses> _levelUpBonuses;

        public Sprite Ico => _ico;
        public string Name => _name;
        public string Description => _description;
        public List<StatData> DefaultStatsData => _defaultStatsData;
        public List<LevelUpBonuses> LevelUpBonuses => _levelUpBonuses;
        public int MaxLvl => _levelUpBonuses.Count + 1;
    }
}