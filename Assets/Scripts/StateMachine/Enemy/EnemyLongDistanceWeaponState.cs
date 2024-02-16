using System;
using Enemy;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class EnemyLongDistanceWeaponState : IState
    {
        private readonly EnemyStateMachineController _enemyStateMachine;
        
        public States CurrentState => States.LongDistanceWeapon;
        
        public event Action<States> ChangeStateEvent;

        public EnemyLongDistanceWeaponState(EnemyStateMachineController enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter()
        {
            Debug.Log($"Enter State EnemyLongDistanceWeaponState");
        }

        public void Update()
        {
            var distance = _enemyStateMachine.MovementController.DistanceToPlayer;
            
            Debug.Log($"distance {distance}");
            
            if (distance < 15f)
            {
                ChangeStateEvent?.Invoke(States.Chaise);        
            }
        }

        public void Exit()
        {
            Debug.Log($"Enter State EnemyLongDistanceWeaponState");
        }
    }
}