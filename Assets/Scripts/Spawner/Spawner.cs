using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<Transform> _spawners;
        [SerializeField] private GemSpawner _gemSpawner;
        [SerializeField] private UnityEvent _enemyDie;
        [SerializeField] private UnityEvent<string> _showKilledEnemyCount;

        private int _enemyCount;
        private int _enemyCountPerSpawn;
        private WaitForSeconds _delay;
        private int _killedEnemyCount;

        private int _radius;

        private void Awake()
        {
            _delay = new WaitForSeconds(1f);
        }

        private void Start()
        {
            StartCoroutine(SpawnPerDelay());
        }

        public void UpdateSpawnDelay(float value)
        {
            _delay = new WaitForSeconds(value);
        }

        public void EnemyDieEventHandler()
        {
            if (_enemyCount <= 0) throw new Exception("No live enemy");
            _enemyCount--;
        }

        private IEnumerator SpawnPerDelay()
        {
            while (true)
            {
                yield return _delay;
                Spawn();
                _enemyCount++;
            }
        }

        private void Spawn()
        {
            var spawnPos = GetRandomPos();
            spawnPos.z = 0;
            var instance = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity, transform);
            SetUpInstance(instance);
        }

        private Vector3 GetRandomPos()
        {
            var x = Random.Range(0, 4);
            return _spawners[x].position;
        }

        private void SetUpInstance(GameObject instance)
        {
            var moveController = instance.GetComponent<EnemyMovementController>();
            moveController.SetPlayer(_player);
            var healthController = instance.GetComponent<EnemyHealthController>();
            healthController.AddDieListener(_gemSpawner.Spawn);
            healthController.EnemyDie.AddListener(_ => _enemyDie.Invoke());
            healthController.EnemyDie.AddListener(_ => EnemyDied());
        }

        private void EnemyDied()
        {
            _killedEnemyCount++;
            _showKilledEnemyCount.Invoke($"x{_killedEnemyCount}");
        }
    }
}