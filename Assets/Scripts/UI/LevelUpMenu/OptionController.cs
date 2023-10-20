using Stats;
using TMPro;
using UI.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelUpMenu
{
    public class OptionController : MonoBehaviour
    {
        [Header("Sprite for weapon")] [SerializeField]
        private Sprite _weaponFrame;

        [SerializeField] private Sprite _weaponBackground;

        [Header("Sprite for Items")] [SerializeField]
        private Sprite _itemFrame;

        [SerializeField] private Sprite _itemBackground;

        [Header("Component reference")]
        [SerializeField] private Image _ico;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _button;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _background;

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

            SetFrameAndBackground(data);
            SetDescription(data);
        }

        private void SetFrameAndBackground(BonusData data)
        {
            if (data.IsWeapon)
            {
                _frame.sprite = _weaponFrame;
                _background.sprite = _weaponBackground;
            }
            else
            {
                _frame.sprite = _itemFrame;
                _background.sprite = _itemBackground;
            }
        }

        private void SetDescription(BonusData data)
        {
            if (data.Level == 0)
            {
                _level.text = "";
                _description.text = $"New weapon {data.StatsData.Name} {data.StatsData.Description}";
            }
            else _description.text = ConvertStatDataToDescription(data.StatsData.LevelUpBonuses[data.Level - 2]);
        }

        private string ConvertStatDataToDescription(LevelUpBonuses stat)
        {
            var description = "";

            foreach (var statData in stat.BonusStat)
            {
                description += $"Add {statData.Value} to {statData.Stat}\n";
            }

            return description;
        }
    }
}