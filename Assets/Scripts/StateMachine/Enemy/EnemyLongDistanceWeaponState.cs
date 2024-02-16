using System;
using System.Collections.Generic;
using Enemy;
using ScriptableObjects;
using Stats.Instances.Buff;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class EnemyLongDistanceWeaponState : IState
    {
        private readonly EnemyStateMachineController _enemyStateMachine;
        private List<TimedBuffInstance> _buffInstance;
        
        public States CurrentState => States.LongDistanceWeapon;
        
        public event Action<States> ChangeStateEvent;
        
        public List<BuffData> Buffs { get; set; }

        public EnemyLongDistanceWeaponState(EnemyStateMachineController enemyStateMachine, List<BuffData> buffs)
        {
            _enemyStateMachine = enemyStateMachine;
            Buffs = buffs;
            
            _buffInstance = new List<TimedBuffInstance>();
        }

        public void Enter()
        {
            Debug.Log($"Enter State EnemyLongDistanceWeaponState");
            
            foreach (var buff in Buffs)
                _buffInstance.Add(_enemyStateMachine.BuffController.AddBuff(buff));
        }

        public void Update()
        {
            var distance = _enemyStateMachine.MovementController.DistanceToPlayer;
            
            if (distance < 15f)
            {
                ChangeStateEvent?.Invoke(States.Chaise);        
            }
        }

        public void Exit()
        {
            Debug.Log($"Enter State EnemyLongDistanceWeaponState");
            
            foreach (var buffInstance in _buffInstance)
                buffInstance.StopBuff();

            _buffInstance = new List<TimedBuffInstance>();
        }
    }
}