using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private UnityEvent _showPanelEvent;
        [SerializeField] private UnityEvent _hidePanelEvent;
        [SerializeField] private UnityEvent _resumeGameEvent;

        public void ResumeGame()
        {
            _resumeGameEvent.Invoke();
        }

        public void ShowPanelEventHandler()
        {
            Show();
            _showPanelEvent.Invoke();
        }
        
        public void HidePanelEventHandler()
        {
            _hidePanelEvent.Invoke();
            // Hide();
        }
        
        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}