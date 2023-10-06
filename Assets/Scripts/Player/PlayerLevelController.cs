using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerLevelController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _levelUpEvent;
        [SerializeField] private UnityEvent _levelUpEndEvent;
        [SerializeField] private UnityEvent<float> _xpChangeEvent;  

        private int _currentLevel = 1;
        private int _currentXp;
        private int _nextLevelXpRequire = 5;

        public void IncreaseXp(int xp)
        {
            _currentXp += xp;

            if (_currentXp >= _nextLevelXpRequire)
            {
                _currentXp -= _nextLevelXpRequire;
                LevelUp();
            }
            
            var currentXpInPercent = CalculateXpInPercent();
            Debug.Log($"currentXpInPercent {currentXpInPercent}");
            _xpChangeEvent.Invoke(currentXpInPercent);
        }
        
        public void LevelUp()
        {
            _levelUpEvent.Invoke();
            _currentLevel++;
            ChangeRequireXp();
        }

        public void LevelUpEnd()
        {
            _levelUpEndEvent.Invoke();
        }

        private void ChangeRequireXp()
        {
            if (_currentLevel > 2 & _currentLevel <= 20)
            {
                _nextLevelXpRequire += 10;
            }
            else if (_currentLevel > 20 & _currentLevel <= 40)
            {
                _nextLevelXpRequire += 13;
            }
            else if(_currentLevel > 40)
            {
                _nextLevelXpRequire += 16;
            }
        }

        private float CalculateXpInPercent() => (((float)_currentXp) / _nextLevelXpRequire);
    }
}