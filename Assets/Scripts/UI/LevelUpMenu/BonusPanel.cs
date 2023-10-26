using System.Collections.Generic;
using UI.Structs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.LevelUpMenu
{
    public class BonusPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private List<OptionController> _options;

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(_firstSelected);

        }
        
        public void SetupButtonsClick(ProgressionPanel.SelectBonusDelegate selectBonus)
        {
            foreach (var option in _options)
            {
                option.SetupButtonClick(selectBonus);
            }
        }
        
        public void SetBonus(BonusData[] data)
        {
            if (data is null)
            {
                Debug.LogWarning($"Bonus data is null");
                return;
            }
            
            if (data.Length != _options.Count)
            {
                Debug.LogWarning($"Data length and _options count are not equal");
                return;
            }

            var emptyCount = 0;
            for (int i = 0; i < _options.Count; i++)
            {
                if (data[i].StatsData == null)
                {
                    emptyCount++;
                    _options[i].SetEmpty("", "", false);
                }
                else
                {
                    _options[i].SetData(data[i]);
                }
            }

            if (emptyCount is 3)
            {
                _options[1].SetEmpty("Congrats", "All weapon and items have max level", true);
            }
        }
        
    }
}