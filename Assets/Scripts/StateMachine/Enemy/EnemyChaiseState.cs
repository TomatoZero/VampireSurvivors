using System;
using Enemy;

namespace StateMachine.Enemy
{
    public class EnemyChaiseState : IState
    {
        private EnemyStateMachineController _enemyStateMachine;
        
        public States CurrentState => States.Chaise;

        public EnemyChaiseState(EnemyStateMachineController enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }
        
        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}