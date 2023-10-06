using System;
using Interface;
using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class MagnetController : MonoBehaviour, IUpdateStats
    {
        [SerializeField] private LayerMask _gemLayer;
        [SerializeField] private UnityEvent<int> _pickUpGemEvent;

        private float _area;
        private Vector3 _scale;

        private void Awake()
        {
            _scale = transform.localScale;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _gemLayer) != 0)
            {
                Destroy(other.gameObject);
                _pickUpGemEvent.Invoke(5);
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
            transform.localScale += new Vector3(_area + 3, _area + 3, 0);
        }
    }
}