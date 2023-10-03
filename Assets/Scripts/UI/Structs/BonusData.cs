using Stats;
using UnityEngine.UI;

namespace UI.Structs
{
    public struct BonusData
    {
        private Image _ico;
        private string _name;
        private string _level;
        private string _description;

        public Image Ico => _ico;
        public string Name => _name;
        public string Level => _level;
        public string Description => _description;

        public BonusData(Image ico, string name, int level, StatData statData)
        {
            _ico = ico;
            _name = name;
            _level = $"New level {level}";
            _description = "";
            _description = ConvertStatDataToDescription(statData);
        }

        private string ConvertStatDataToDescription(StatData stat)
        {
            return $"Add {stat.Value} to {stat.Stat}";
        }
    }
}