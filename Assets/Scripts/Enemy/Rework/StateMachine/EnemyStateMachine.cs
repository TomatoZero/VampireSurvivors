using System;
using Interface;

namespace Enemy.Rework.StateMachine
{
    [Serializable]
    public class EnemyStateMachine
    {
        private IState _currentState;

        public void Initialize(IState startState)
        {
            _currentState = startState;
            startState.Enter();
        }

        public void TransitionTo(IState nextState)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        public void Update()
        {
            if(_currentState is not null)
                _currentState.Update();
        }
    }
}
