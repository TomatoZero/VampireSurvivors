using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private UnityEvent _resumeGameEvent;

        public void ResumeGame()
        {
            _resumeGameEvent.Invoke();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}