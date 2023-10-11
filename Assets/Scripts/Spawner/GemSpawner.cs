using System;
using PickUpItems;
using PickUpItems.Gem;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class GemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _gemPrefab;

        public delegate void SpawnGem(Vector3 pos);
        public delegate void Magnet(Transform player, float speed);

        public event Magnet MagnetEvent;

        public void MagnetPickUp(Transform player, float speed)
        {
            MagnetEvent.Invoke(player, speed);
            UnsubscribeFromEvent();
        }

        public void Spawn(Vector3 pos)
        {
            var instance = Instantiate(_gemPrefab, pos, quaternion.identity, transform);
            
            if (instance.TryGetComponent(out GemDataController gemData))
            {
                gemData.Setup(GetXpBonus());
            }

            if (instance.TryGetComponent(out PickUpItemMoveController gemMove))
            {
                MagnetEvent += gemMove.EnableItem;
                gemMove.ItemDestroyEvent += Unsubscribe;
            }
        }

        private int GetXpBonus()
        {
            return 5;
        }

        private void UnsubscribeFromEvent()
        {
            Delegate[] clientList = MagnetEvent.GetInvocationList();
            foreach (var d in clientList)
                MagnetEvent -= (d as Magnet);
        }
        
        private void Unsubscribe(Magnet magnetEvent)
        {
            MagnetEvent -= magnetEvent;
        }
    }
}