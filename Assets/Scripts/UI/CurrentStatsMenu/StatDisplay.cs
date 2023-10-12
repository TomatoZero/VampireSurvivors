using Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.CurrentStatsMenu
{
    public class StatDisplay : MonoBehaviour
    {
        [SerializeField] private Image _ico;
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statValue;

        public void Setup(string statName, string statValue)
        {
            _statName.text = statName;
            _statValue.text = statValue;
        }
    }
}