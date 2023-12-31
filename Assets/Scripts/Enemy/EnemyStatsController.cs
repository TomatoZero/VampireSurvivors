﻿using Stats.Instances;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyStatsController : MonoBehaviour
    {
        [SerializeField] private EnemyStatsData _enemyStatsData;
        [SerializeField] private UnityEvent<EnemyInstance> _setupStatsEvent;
        [SerializeField] private UnityEvent<EnemyInstance> _statsUpdateEvent;

        private EnemyInstance _instance;

        private void Awake()
        {
            _instance = new EnemyInstance(_enemyStatsData);
            _setupStatsEvent.Invoke(_instance);
        }
    }
}