using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateMachine
{
    [Serializable]
    public class EnemyStateMachine
    {
        private IState _currentState;
        private List<IState> _states;
        
        public EnemyStateMachine(List<IState> states, States startState)
        {
            _states = states;
            SubscribeToEvents();
            
            _currentState = FindState(startState);
            _currentState.Enter();
        }

        public void TransitionTo(States nextState)
        {
            _currentState.Exit();
            _currentState = FindState(nextState);
            _currentState.Enter();
        }

        public void Update()
        {
            if (_currentState is not null)
                _currentState.Update();
        }

        private IState FindState(States states)
        {
            var newState = _states.First(state => state.CurrentState == states);

            if (newState is null)
            {
                Debug.LogError($"State not found");
                return _currentState;
            }
            
            return newState;
        }

        private void SubscribeToEvents()
        {
            foreach (var state in _states)
                state.ChangeStateEvent += TransitionTo;
        }
    }
}