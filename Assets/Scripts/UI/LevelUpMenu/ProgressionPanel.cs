using UI.Structs;
using UnityEngine;
using UnityEngine.Events;

namespace UI.LevelUpMenu
{
    public class ProgressionPanel : MonoBehaviour
    {
        [SerializeField] private UnityEvent<BonusData> _upgradeItemEvent;
        [SerializeField] private UnityEvent _hideLevelUpEvent;
        [SerializeField] private UnityEvent _showEvent;
        [SerializeField] private UnityEvent _hideEvent;
        [SerializeField] private BonusPanel _bonusPanel;
        
        public delegate void SelectBonusDelegate(BonusData bonus);

        private void Awake()
        {
            _bonusPanel.SetupButtonsClick(SelectBonus);
        }

        public void SetupUpgradesEventHandler(BonusData[] upgrade)
        {
            _bonusPanel.SetBonus(upgrade);
        }

        public void Show()
        {
            _showEvent.Invoke();
        }
        
        public void Hide()
        {
            _hideEvent.Invoke();
        }

        private void SelectBonus(BonusData bonus)
        {
            _upgradeItemEvent.Invoke(bonus);
            _hideLevelUpEvent.Invoke();
        }
    }
}