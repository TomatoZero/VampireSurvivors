using System;

namespace StateMachine
{
    public interface IState
    {
        public States CurrentState { get; }

        public event Action<States> ChangeStateEvent; 
        
        public void Enter();
        public void Update();
        public void Exit();
    }
}