using System;
using Stats.Instances;
using UI.Structs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<PlayerInstance> _updateStats;
        [FormerlySerializedAs("_showUpdates")] [SerializeField] private UnityEvent<BonusData[]> _setUpdates;
        [SerializeField] private UnityEvent<BonusData> _upgradeItemEvent;
        [SerializeField] private UnityEvent _hideAllExceptHUDEvent;
        [SerializeField] private UnityEvent _showLevelUpUiEvent;
        [Space, Header("PauseMenu")]
        [SerializeField] private UnityEvent _showPauseMenu;
        [SerializeField] private UnityEvent _hidePauseMenu;

        private bool _isUpgradeOpen;
        private bool _isStatMenuOpen;

        private void Awake()
        {
            _isUpgradeOpen = false;
            _isStatMenuOpen = false;
        }

        public void UpdateStatEventHandler(PlayerInstance instance)
        {
            _updateStats.Invoke(instance);
        }

        public void ShowUpdatesEventHandler(BonusData[] bonusData)
        {
            _setUpdates.Invoke(bonusData);
            _showLevelUpUiEvent.Invoke();
            _isUpgradeOpen = true;
        }

        public void HideAllExceptHUD()
        {
            _isUpgradeOpen = false;
            _isStatMenuOpen = false;
            _hideAllExceptHUDEvent.Invoke();
        }

        public void UpgradeItemEventHandler(BonusData data)
        {
            _upgradeItemEvent.Invoke(data);
            _hideAllExceptHUDEvent.Invoke();
        }

        public void TurnPauseMenu()
        {
            if(_isUpgradeOpen) return;
            
            if (_isStatMenuOpen)
            {
                _hidePauseMenu.Invoke();
                _isStatMenuOpen = false;
            }
            else
            {
                _showPauseMenu.Invoke();
                _isStatMenuOpen = true;
            }
        }
    }
}