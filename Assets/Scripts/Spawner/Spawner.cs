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
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<Transform> _spawners;
        [SerializeField] private GemSpawner _gemSpawner;
        [SerializeField] private UnityEvent _enemyDie;
        [SerializeField] private UnityEvent<string> _showKilledEnemyCount;

        private int _enemyCount;
        private int _enemyCountPerSpawn;
        private WaitForSeconds _delay;
        private float _delayF;
        private int _killedEnemyCount;

        private int _radius;

        private void Awake()
        {
            _delayF = 1.5f;
            _delay = new WaitForSeconds(1.5f);
        }

        private void Start()
        {
            StartCoroutine(SpawnPerDelay());
        }

        private IEnumerator ChangeSpawnDelayPerMinute()
        {
            while (true)
            {
                yield return new WaitForSeconds(30);
                UpdateSpawnDelay(_delayF -= .2f);
            }
        }
        
        public void UpdateSpawnDelay(float value)
        {
            _delayF = value;
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

            // var enemyEvent = instance.GetComponent<EnemyEventController>();
            // enemyEvent.Instantiate(_player.transform, DieMethod);
        }

        private void DieMethod(Vector3 arg0)
        {
            throw new NotImplementedException();
        }

        private void EnemyDied()
        {
            _killedEnemyCount++;
            _showKilledEnemyCount.Invoke($"x{_killedEnemyCount}");
        }
    }
}