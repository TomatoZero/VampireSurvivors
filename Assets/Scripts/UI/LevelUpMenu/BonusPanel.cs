using System.Collections.Generic;
using UI.Structs;
using UnityEngine;

namespace UI.LevelUpMenu
{
    public class BonusPanel : MonoBehaviour
    {
        [SerializeField] private List<OptionController> _options;

        public void SetBonus(BonusData[] data)
        {
            if(data is null)
            {
                Debug.LogWarning($"Bonus data is null");
                return;
            }
            if(data.Length != _options.Count)
            {
                Debug.LogWarning($"Data length and _options count are not equal");
                return;
            }

            for (int i = 0; i < _options.Count; i++)
            {
                _options[i].SetData(data[i]);
            }
        }
    }
}