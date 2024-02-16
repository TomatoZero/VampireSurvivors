using System;
using Enemy;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class EnemyMeleeWeaponState : IState
    {
        private readonly EnemyStateMachineController _enemyStateMachine;
        
        public States CurrentState => States.MeleeWeapon;
        
        public event Action<States> ChangeStateEvent;

        public EnemyMeleeWeaponState(EnemyStateMachineController enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log($"Enter State EnemyMeleeWeaponState");
        }

        public void Update()
        {
            var distance = _enemyStateMachine.MovementController.DistanceToPlayer;
            
            Debug.Log($"distance {distance}");
            
            if (distance is > 3f and < 15f)
            {
                ChangeStateEvent?.Invoke(States.Chaise);        
            }
            else if(distance > 15f)
            {
                ChangeStateEvent?.Invoke(States.LongDistanceWeapon);        
            }
        }

        public void Exit()
        {
            Debug.Log($"Exit State EnemyMeleeWeaponState");
        }
    }
}