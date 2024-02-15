using System;

namespace StateMachine
{
    public interface IState
    {
        public States CurrentState { get; }
        
        public void Enter();
        public void Update();
        public void Exit();
    }
}