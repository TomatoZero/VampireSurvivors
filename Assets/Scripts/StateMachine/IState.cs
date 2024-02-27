using System;
using System.Collections.Generic;
using ScriptableObjects;

namespace StateMachine
{
    public interface IState
    {
        public States CurrentState { get; }

        public event Action<States> ChangeStateEvent;

        public List<BuffData> Buffs { get; set; }
        
        public void Enter();
        public void Update();
        public void Exit();
    }
}