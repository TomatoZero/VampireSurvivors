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

        public void SetData(BonusData data)
        {
            _ico.sprite = data.Ico;
            _name.text = data.Name;
            _level.text = data.Level;
            _description.text = data.Description;
        }
    }
}