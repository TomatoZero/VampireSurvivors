using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Melee.LightningRing
{
    public class LightningRingShootController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private UnityEvent<Collider2D[]> _hitEnemy;
        [SerializeField] private UnityEvent<Vector2[]> _hitPosition;
        [SerializeField] private UnityEvent _startTimerEvent;

        private readonly Vector2 _boxSize = new Vector2(35, 20);

        private int _amount;
        private int _area;

        private Vector2[] _enemyLightningHitPosition;
        private List<Collider2D> _allEnemyLightningHit;

        private protected void Awake()
        {
            _enemyLightningHitPosition = Array.Empty<Vector2>();
            _allEnemyLightningHit = new List<Collider2D>();
        }

        public void Shoot()
        {
            StartCoroutine(ShootWithDelay());
        }

        private IEnumerator ShootWithDelay()
        {
            ScanForEnemyOnScreen();
            
            if (_allEnemyLightningHit is null || _allEnemyLightningHit.Count == 0)
            {
                _startTimerEvent.Invoke();
                yield break;
            }
            
            yield return new WaitForSeconds(.1f);

            _hitEnemy.Invoke(_allEnemyLightningHit.ToArray());
            _startTimerEvent.Invoke();
        }
        
        private void ScanForEnemyOnScreen()
        {
            _allEnemyLightningHit = new List<Collider2D>();
            var allEnemyAtScreen = Physics2D.OverlapBoxAll(transform.position, _boxSize, 0, _layerMask);

            if (allEnemyAtScreen is null || allEnemyAtScreen.Length == 0)
            {
                _allEnemyLightningHit = new List<Collider2D>();
                return;
            }

            AddEnemyTarget(allEnemyAtScreen);
            SetShootPositions();
            AddEnemyAroundTarget();
        }

        private void AddEnemyTarget(Collider2D[] enemyAtScreen)
        {
            if (enemyAtScreen.Length < _amount)
            {
                for (int i = 0; i < enemyAtScreen.Length - 1; i++)
                {
                    _enemyLightningHitPosition[i] = enemyAtScreen[i].transform.position;
                }

                return;
            }

            var step = enemyAtScreen.Length / _amount;

            for (int i = 0, j = 0; j < _amount & i < enemyAtScreen.Length - 1; i += step, j++)
            {
                _enemyLightningHitPosition[j] = enemyAtScreen[i].transform.position;
            }
        }

        private void AddEnemyAroundTarget()
        {
            for (int i = 0; i < _amount; i++)
            {
                if(_enemyLightningHitPosition[i].Equals(null)) continue;
                
                var additionalEnemy = ScanForEnemyInCircle(_enemyLightningHitPosition[i]);

                if (additionalEnemy.Length == 0) continue;

                _allEnemyLightningHit.AddRange(additionalEnemy);
            }
        }

        private void SetShootPositions()
        {
            _hitPosition.Invoke(_enemyLightningHitPosition);
        }

        private Collider2D[] ScanForEnemyInCircle(Vector2 position)
        {
            return Physics2D.OverlapCircleAll(position, _area / 2, _layerMask) ?? Array.Empty<Collider2D>();
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _amount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _area = (int)newInstance.GetStatByName(Stats.Stats.Area).Value;
            _enemyLightningHitPosition = new Vector2[_amount];
            _startTimerEvent.Invoke();
            
            Debug.Log($"Area {_area}");
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _amount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _area = (int)newInstance.GetStatByName(Stats.Stats.Area).Value;
            _enemyLightningHitPosition = new Vector2[_amount];
            
            Debug.Log($"Area {_area}");
        }
    }
}