using System.Collections.Generic;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachineController : MonoBehaviour
    {
        [SerializeField] private EnemyStateMachineController _enemyStateMachineController;
        [SerializeField] private List<States> _states;

        private EnemyStateMachine _stateMachine;

        private void Awake()
        {
            var states = new List<IState>();
        }
    }
}