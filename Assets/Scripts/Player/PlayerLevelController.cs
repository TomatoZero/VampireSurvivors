using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerLevelController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _levelUpEvent;
        [SerializeField] private UnityEvent _levelUpEndEvent;
        
        public void LevelUp()
        {
            _levelUpEvent.Invoke();
        }

        public void LevelUpEnd()
        {
            _levelUpEndEvent.Invoke();
        }
    }
}