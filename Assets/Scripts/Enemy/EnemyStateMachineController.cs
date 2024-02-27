using System.Collections.Generic;
using Controllers;
using Enemy.EnemyWeapons;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyStateMachineController : MonoBehaviour
    {
        [SerializeField] private EnemyReference _enemyReference;

        public EnemyMovementController MovementController => _enemyReference.MovementController;
        public BuffController BuffController => _enemyReference.BuffController;
        public EnemyWeaponController WeaponControl => _enemyReference.WeaponController;

        protected EnemyStateMachine StateMachine { get; set; }
        protected List<IState> States { get; set; }

        protected abstract void CreateStates();
        protected abstract void CreateStateMachine();
    }
}