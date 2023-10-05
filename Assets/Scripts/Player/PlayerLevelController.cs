using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerLevelController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _levelUpEvent;
        
        public void LevelUp()
        {
            _levelUpEvent.Invoke();
        }
    }
}