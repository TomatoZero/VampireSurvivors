using System;
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
        [SerializeField] private UnityEvent _startTimerEvent;

        private readonly Vector2 _boxSize = new Vector2(35, 20);

        private int _amount;
        private int _area;
        
        private Collider2D[] _enemyLightningHit;
        private List<Collider2D> _allEnemyLightningHit;

        private protected void Awake()
        {
            _enemyLightningHit = Array.Empty<Collider2D>();
            _allEnemyLightningHit = new List<Collider2D>();
        }

        public void Shoot()
        {
            ScanForEnemyOnScreen();
            
            if (_allEnemyLightningHit is null || _allEnemyLightningHit.Count == 0)
            {
                _startTimerEvent.Invoke();
                return;
            }
            
            _hitEnemy.Invoke(_allEnemyLightningHit.ToArray());
            _startTimerEvent.Invoke();
        }

        private void ScanForEnemyOnScreen()
        {
            _allEnemyLightningHit = new List<Collider2D>();
            var allEnemyAtScreen = Physics2D.OverlapBoxAll(transform.position, _boxSize, 0, _layerMask);
            
            if(allEnemyAtScreen is null || allEnemyAtScreen.Length == 0)
            {
                _allEnemyLightningHit = new List<Collider2D>();
                return;
            }
            
            var step = allEnemyAtScreen.Length / _amount;
            
            for (int i = 0, j = 0; i < allEnemyAtScreen.Length - 1 || j < _amount ; i += step, j++)
            {
                _enemyLightningHit[j] = allEnemyAtScreen[i];
            }

            Collider2D[] additionalEnemy;
            
            foreach (var lightningHit in _enemyLightningHit)
            {
                additionalEnemy = ScanForEnemyInCircle(lightningHit.transform.position);
                
                Debug.Log($"additionalEnemy {additionalEnemy.Length} ");
                
                if(additionalEnemy.Length == 0) continue;
                
                _allEnemyLightningHit.AddRange(additionalEnemy);
            }
        }

        private Collider2D[] ScanForEnemyInCircle(Vector2 position)
        {
            return Physics2D.OverlapCircleAll(position, _area / 2, _layerMask) ?? Array.Empty<Collider2D>();
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _amount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _area = (int)newInstance.GetStatByName(Stats.Stats.Area).Value;
            _enemyLightningHit = new Collider2D[_amount];
            _startTimerEvent.Invoke();
        }

        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _amount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _area = (int)newInstance.GetStatByName(Stats.Stats.Area).Value;
            _enemyLightningHit = new Collider2D[_amount];
        }
    }
}