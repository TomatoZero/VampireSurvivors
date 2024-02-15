using System.Collections.Generic;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyStateMachineController : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController _movementController;

        protected EnemyMovementController MovementController
        {
            get => _movementController;
            set => _movementController = value;
        }
        
        protected EnemyStateMachine StateMachine { get; set; }
        protected List<IState> States { get; set; }

        protected abstract void CreateStates();
        protected abstract void CreateStateMachine();
    }
}