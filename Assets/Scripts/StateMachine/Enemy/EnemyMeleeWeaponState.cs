using System;
using System.Collections.Generic;
using Enemy;
using ScriptableObjects;
using Stats.Instances.Buff;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class EnemyMeleeWeaponState : IState
    {
        private readonly EnemyStateMachineController _enemyStateMachine;
        private List<TimedBuffInstance> _buffInstance;
        
        public States CurrentState => States.MeleeWeapon;
        public event Action<States> ChangeStateEvent;
        
        public List<BuffData> Buffs { get; set; }

        public EnemyMeleeWeaponState(EnemyStateMachineController enemyStateMachine, List<BuffData> buffs)
        {
            _enemyStateMachine = enemyStateMachine;
            Buffs = buffs;
            
            _buffInstance = new List<TimedBuffInstance>();
        }
        
        public void Enter()
        {
            Debug.Log($"Enter State EnemyMeleeWeaponState");

            _buffInstance = new List<TimedBuffInstance>();
            
            foreach (var buff in Buffs)
                _buffInstance.Add(_enemyStateMachine.BuffController.AddBuff(buff));
        }

        public void Update()
        {
            var distance = _enemyStateMachine.MovementController.DistanceToPlayer;
            
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

            foreach (var buffInstance in _buffInstance)
                buffInstance.StopBuff();

            _buffInstance = new List<TimedBuffInstance>();
        }
    }
}