using UI.Structs;
using UnityEngine;
using UnityEngine.Events;

namespace UI.LevelUpMenu
{
    public class ProgressionPanel : MonoBehaviour
    {
        [SerializeField] private UnityEvent<BonusData> _upgradeItemEvent;
        [SerializeField] private UnityEvent _hideLevelUpEvent;
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
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SelectBonus(BonusData bonus)
        {
            _upgradeItemEvent.Invoke(bonus);
            _hideLevelUpEvent.Invoke();
        }
    }
}