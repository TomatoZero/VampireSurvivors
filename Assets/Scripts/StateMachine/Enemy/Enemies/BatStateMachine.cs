using Enemy;

namespace StateMachine.Enemy.Enemies
{
    public class BatStateMachine : EnemyStateMachineController
    {
        private void Start()
        {
            CreateStates();
            CreateStateMachine();
        }

        protected override void CreateStates()
        {
            States.Add(new EnemyChaiseState(this));
        }

        protected override void CreateStateMachine()
        {
            StateMachine = new EnemyStateMachine(States, global::StateMachine.States.Chaise);
        }
    }
}