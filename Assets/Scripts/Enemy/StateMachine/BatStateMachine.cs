using System.Collections.Generic;
using ScriptableObjects;
using StateMachine;
using StateMachine.Enemy;
using UnityEngine;

namespace Enemy.StateMachine
{
    public class BatStateMachine : EnemyStateMachineController
    {
        [Header("Buffs")]

        [SerializeField] private List<BuffData> _chaiseStateBuff;
        [SerializeField] private List<BuffData> _meleeWeaponStateBuff;
        [SerializeField] private List<BuffData> _longDistanceWeaponStateBuff;
        
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
            
            States.Add(new EnemyChaiseState(this, _chaiseStateBuff));
            States.Add(new EnemyMeleeWeaponState(this, _meleeWeaponStateBuff));
            States.Add(new EnemyLongDistanceWeaponState(this, _longDistanceWeaponStateBuff));
        }

        protected override void CreateStateMachine()
        {
            StateMachine = new EnemyStateMachine(States, global::StateMachine.States.Chaise);
        }
    }
}