using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private List<Vector2> _spawnModifiers;
        [SerializeField] private UnityEvent<Vector3> _enemyDied;

        private Dictionary<GameObject, Queue<EnemyEventController>> _objectPulls;

        public void Spawn(GameObject enemyPrefab)
        {
            if (_objectPulls.TryGetValue(enemyPrefab, out var pull))
            {
                if (pull.TryDequeue(out var enemy))
                    ActivateEnemy(enemy);
                else
                    CreateEnemy(enemyPrefab);
            }
            else
            {
                var newQueue = new Queue<EnemyEventController>();
                _objectPulls.Add(enemyPrefab, newQueue);
                CreateEnemy(enemyPrefab);
            }
        }

        private EnemyEventController CreateEnemy(GameObject enemyPrefab)
        {
            var spawnPosition = _player.position + (Vector3)GetRandomPositionModifier();
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            var enemyEventModifier = enemy.GetComponent<EnemyEventController>();
            enemyEventModifier.Instantiate(_player, EnemyDied);

            return enemyEventModifier;
        }

        private void ActivateEnemy(EnemyEventController enemy)
        {
            var spawnPosition = _player.position + (Vector3)GetRandomPositionModifier();
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
        }

        private void EnemyDied(EnemyEventController eventController)
        {
            if (_objectPulls.TryGetValue(eventController.gameObject, out var pull))
            {
                eventController.gameObject.SetActive(false);
                pull.Enqueue(eventController);
            }
            else
            {
                eventController.gameObject.SetActive(false);
                Debug.LogError("Cant find pull");
            }

            _enemyDied.Invoke(eventController.transform.position);
        }

        private Vector2 GetRandomPositionModifier()
        {
            var x = Random.Range(0, _spawnModifiers.Count);
            return _spawnModifiers[x];
        }
    }
}