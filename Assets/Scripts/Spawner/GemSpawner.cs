using PickUpItems.Gem;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class GemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _gemPrefab;

        public delegate void SpawnGem(Vector3 pos);
        public delegate void Magnet();

        public event Magnet MagnetEvent;

        public void MagnetPickUp()
        {
            MagnetEvent.Invoke();
        }

        public void Spawn(Vector3 pos)
        {
            var instance = Instantiate(_gemPrefab, pos, quaternion.identity, transform);
            
            if (instance.TryGetComponent(out GemDataController gemData))
            {
                gemData.Setup(GetXpBonus());    
            }
        }

        private int GetXpBonus()
        {
            return 5;
        }
    }
}