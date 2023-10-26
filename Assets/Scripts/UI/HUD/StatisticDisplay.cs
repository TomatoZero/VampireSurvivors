using UnityEngine;
using UnityEngine.Events;

namespace UI.HUD
{
    public class StatisticDisplay : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string> _killedEnemyCountEvent;
        [SerializeField] private UnityEvent<string> _playerLevelChange;
        [SerializeField] private UnityEvent<string> _updatePassedTime;

        private int _level = 1;

        public void KilledEnemy(string killedEnemy)
        {
            _killedEnemyCountEvent.Invoke(killedEnemy);
        }

        public void LevelUp()
        {
            _level++;
            _playerLevelChange.Invoke(_level.ToString());
        }

        public void UpdatePassedTime(string seconds)
        {
            _updatePassedTime.Invoke(seconds);
        }
    }
}