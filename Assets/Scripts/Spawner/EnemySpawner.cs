using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int _initEnemyCount;
        [SerializeField] private Transform _player;
        [SerializeField] private List<Vector2> _spawnModifiers;
        [SerializeField] private GameObject _spawnObject;
        [SerializeField] private UnityEvent<Vector3> _enemyDied;

        private Queue<EnemyEventController> _unActiveEnemies;

        private void Awake()
        {
            for (int i = 0; i < _initEnemyCount; i++)
            {
                var enemy = CreateEnemy();
                enemy.gameObject.SetActive(false);
            }
        }

        private void Spawn()
        {
            if (_unActiveEnemies.TryDequeue(out EnemyEventController enemy))
            {
                enemy.gameObject.SetActive(true);
            }
            else
            {
                CreateEnemy();
            }
        }
        
        private EnemyEventController CreateEnemy()
        {
            var spawnPosition = _player.position + (Vector3)GetRandomPositionModifier();
            var enemy = Instantiate(_spawnObject, spawnPosition, Quaternion.identity, transform);
            var enemyEventModifier = enemy.GetComponent<EnemyEventController>();
            enemyEventModifier.Instantiate(_player, EnemyDied);

            return enemyEventModifier;
        }

        private void EnemyDied(EnemyEventController eventController)
        {
            eventController.gameObject.SetActive(false);
            _unActiveEnemies.Enqueue(eventController);
            
            _enemyDied.Invoke(eventController.transform.position);
        }
        
        private Vector2 GetRandomPositionModifier()
        {
            var x = Random.Range(0, _spawnModifiers.Count);
            return _spawnModifiers[x];
        }
    }
}