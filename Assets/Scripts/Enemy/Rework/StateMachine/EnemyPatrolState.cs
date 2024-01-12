using Interface;

namespace Enemy.Rework.StateMachine
{
    public class EnemyPatrolState : IState
    {
        private float _patrolTime;
        private float _patrolLeftTime;
        
        public void Enter()
        {
            _patrolLeftTime = _patrolTime;
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}