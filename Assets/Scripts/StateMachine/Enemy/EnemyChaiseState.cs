using System;
using System.Collections.Generic;
using Enemy;
using ScriptableObjects;
using Stats.Instances.Buff;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class EnemyChaiseState : IState
    {
        private readonly EnemyStateMachineController _enemyStateMachine;
        private List<TimedBuffInstance> _buffInstance;
        
        public States CurrentState => States.Chaise;
        public event Action<States> ChangeStateEvent;
        
        public List<BuffData> Buffs { get; set; }

        public EnemyChaiseState(EnemyStateMachineController enemyStateMachine, List<BuffData> buffs)
        {
            _enemyStateMachine = enemyStateMachine;
            Buffs = buffs;

            _buffInstance = new List<TimedBuffInstance>();
        }

        public void Enter()
        {
            // Debug.Log("Enter State EnemyChaiseState");
            
            foreach (var buff in Buffs)
                _buffInstance.Add(_enemyStateMachine.BuffController.AddBuff(buff));
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
            // Debug.Log("Enter State EnemyChaiseState");
            
            foreach (var buffInstance in _buffInstance)
                buffInstance.StopBuff();

            _buffInstance = new List<TimedBuffInstance>();
        }
    }
}