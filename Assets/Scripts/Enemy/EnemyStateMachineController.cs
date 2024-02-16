using System.Collections.Generic;
using Controllers;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyStateMachineController : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private BuffController _buffController;

        public EnemyMovementController MovementController
        {
            get => _movementController;
            protected set => _movementController = value;
        }

        public BuffController BuffController
        {
            get => _buffController;
            protected set => _buffController = value;
        }

        protected EnemyStateMachine StateMachine { get; set; }
        protected List<IState> States { get; set; }

        protected abstract void CreateStates();
        protected abstract void CreateStateMachine();
    }
}