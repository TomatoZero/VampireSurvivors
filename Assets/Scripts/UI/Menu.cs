using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject _firsSelected;
        [SerializeField] private UnityEvent _showPanelEvent;
        [SerializeField] private UnityEvent _hidePanelEvent;
        [SerializeField] private UnityEvent _resumeGameEvent;

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(_firsSelected);
        }

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