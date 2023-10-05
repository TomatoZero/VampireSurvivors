using UI.Structs;
using UnityEngine;

namespace UI.LevelUpMenu
{
    public class ProgressionPanel : MonoBehaviour
    {
        [SerializeField] private BonusPanel _bonusPanel;
        
        private void SetupUpgradesEventHandler(BonusData[] upgrade)
        {
            gameObject.SetActive(true);
            _bonusPanel.SetBonus(upgrade);
        }
    }
}