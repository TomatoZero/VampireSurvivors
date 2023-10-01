using TMPro;
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

        public Image Ico
        {
            get => _ico;
            set => _ico = value;
        }

        public TMP_Text Name
        {
            get => _name;
            set => _name = value;
        }

        public TMP_Text Level
        {
            get => _level;
            set => _level = value;
        }

        public TMP_Text Description
        {
            get => _description;
            set => _description = value;
        }
    }
}