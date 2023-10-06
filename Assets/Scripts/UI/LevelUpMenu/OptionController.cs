using Stats;
using TMPro;
using UI.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelUpMenu
{
    public class OptionController : MonoBehaviour
    {
        [SerializeField] private Image _ico;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _button;

        private BonusData _data;

        public void SetupButtonClick(ProgressionPanel.SelectBonusDelegate bonus)
        {
            _button.onClick.AddListener(() => bonus(_data));
        }

        public void SetData(BonusData data)
        {
            _data = data;
            _ico.sprite = data.StatsData.Ico;
            _name.text = data.StatsData.Name;
            _level.text = $"New level: {data.Level}";

            if (data.Level == 0) _description.text = $"New weapon {data.StatsData.Name}";
            else _description.text = ConvertStatDataToDescription(data.StatsData.LevelUpBonuses[data.Level - 2]);
        }

        private string ConvertStatDataToDescription(LevelUpBonuses stat)
        {
            var description = "";

            foreach (var statData in stat.BonusStat)
            {
                description += $"Add {statData.Value} to {statData.Stat}";
            }

            return description;
        }
    }
}