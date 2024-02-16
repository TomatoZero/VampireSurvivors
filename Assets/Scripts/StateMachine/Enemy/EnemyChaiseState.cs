using System;
using Enemy;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class EnemyChaiseState : IState
    {
        private readonly EnemyStateMachineController _enemyStateMachine;
        
        public States CurrentState => States.Chaise;
        public event Action<States> ChangeStateEvent;

        public EnemyChaiseState(EnemyStateMachineController enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter()
        {
            Debug.Log($"Enter State EnemyChaiseState");
        }

        public void Update()
        {
            var distance = _enemyStateMachine.MovementController.DistanceToPlayer;
            
            if (distance < 3f )
            {
                ChangeStateEvent?.Invoke(States.MeleeWeapon);        
            }
            else if(distance > 15f)
            {
                ChangeStateEvent?.Invoke(States.LongDistanceWeapon);        
            }
        }

        public void Exit()
        {
            Debug.Log($"Enter State EnemyChaiseState");
        }
    }
}