using System;
using Interface;
using PickUpItems;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class MagnetController : MonoBehaviour, IUpdateStats
    {
        [FormerlySerializedAs("_gemLayer")] [SerializeField] private LayerMask _pickUpItemLayer;
        
        private float _area;
        private Vector3 _scale;

        private void Awake()
        {
            _scale = transform.localScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _pickUpItemLayer) != 0)
            {
                if(other.TryGetComponent(out PickUpItemMoveController moveController))
                {
                    moveController.EnableItem(transform);
                }
            }
        }

        public void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            UpdateLocalScale();
        }


        public void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _area = newInstance.GetStatByName(Stats.Stats.Area).Value;
            UpdateLocalScale();
        }
        
        private void UpdateLocalScale()
        {
            var scale = transform.localScale.y;
            transform.localScale -= new Vector3(scale, scale, 0);
            transform.localScale += new Vector3(_area + 4, _area + 4, 0);
        }
    }
}