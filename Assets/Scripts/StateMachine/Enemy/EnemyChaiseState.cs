using System;
using Enemy;

namespace StateMachine.Enemy
{
    public class EnemyChaiseState : IState
    {
        private EnemyReferenceController _enemyReference;
        
        public States CurrentState => States.Chaise;

        public EnemyChaiseState(EnemyReferenceController enemyReference)
        {
            _enemyReference = enemyReference;
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