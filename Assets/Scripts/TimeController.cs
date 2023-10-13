using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class TimeController : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void TurnOnTime()
        {
            if (Time.timeScale == 1)
            {
                Debug.LogWarning("TimeScale already set to 1");
                return;
            }

            Time.timeScale = 1;
        }

        public void TurnOffTime()
        {
            if (Time.timeScale == 0)
            {
                Debug.LogWarning("TimeScale already set to 0");
                return;
            }

            Time.timeScale = 0;
        }

        public void TurnTime()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        private void OnSceneUnloaded(Scene unloadedScene)
        {
            TurnOnTime();
        }
    }
}