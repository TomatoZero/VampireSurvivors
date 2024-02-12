using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponsOnLevel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weapons;

        public GameObject LoadPrefab(string name)
        {
            foreach (var weapon in _weapons)
            {
                if (weapon.name == name)
                {
                    Debug.Log($"weapon {weapon}");
                    return weapon;
                }
            }

            return null;
        }
    }
}