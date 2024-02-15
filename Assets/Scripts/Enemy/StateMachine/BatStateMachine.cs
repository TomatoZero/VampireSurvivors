using System.Collections.Generic;
using StateMachine;
using StateMachine.Enemy;

namespace Enemy.StateMachine
{
    public class BatStateMachine : EnemyStateMachineController
    {
        private void Start()
        {
            CreateStates();
            CreateStateMachine();
        }

        private void Update()
        {
            StateMachine.Update();
        }

        protected override void CreateStates()
        {
            States = new List<IState>();
            
            States.Add(new EnemyChaiseState(this));
            States.Add(new EnemyMeleeWeaponState(this));
            States.Add(new EnemyLongDistanceWeaponState(this));
        }

        protected override void CreateStateMachine()
        {
            StateMachine = new EnemyStateMachine(States, global::StateMachine.States.Chaise);
        }
    }
}