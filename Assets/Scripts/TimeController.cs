using UnityEngine;

namespace DefaultNamespace
{
    public class TimeController : MonoBehaviour
    {
        public void TurnOnTime()
        {
            if(Time.timeScale == 1)
            {
                Debug.LogWarning("TimeScale already set to 1");
                return;
            }

            Time.timeScale = 1;
        }
        
        public void TurnOffTime()
        {
            if(Time.timeScale == 0)
            {
                Debug.LogWarning("TimeScale already set to 0");
                return;
            }

            Time.timeScale = 0;
        }
        
    }
}