using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private UnityEvent _resumeGameEvent;

        public void ResumeGame()
        {
            _resumeGameEvent.Invoke();
        }

        public void ExitDame()
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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