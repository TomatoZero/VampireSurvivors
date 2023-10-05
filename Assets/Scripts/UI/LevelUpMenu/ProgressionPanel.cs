using UI.Structs;
using UnityEngine;
using UnityEngine.Events;

namespace UI.LevelUpMenu
{
    public class ProgressionPanel : MonoBehaviour
    {
        [SerializeField] private UnityEvent<BonusData> _upgradeItemEvent;
        [SerializeField] private BonusPanel _bonusPanel;

        public delegate void SelectBonusDelegate(BonusData bonus);

        public void SetupUpgradesEventHandler(BonusData[] upgrade)
        {
            gameObject.SetActive(true);
            _bonusPanel.SetBonus(upgrade, SelectBonus);
        }

        public void SelectBonus(BonusData bonus)
        {
            gameObject.SetActive(false);
            _upgradeItemEvent.Invoke(bonus);
        }
    }
}