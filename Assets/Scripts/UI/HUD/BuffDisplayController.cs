using System;
using Stats.Instances.Buff;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.HUD
{
    public class BuffDisplayController : MonoBehaviour
    {
        [SerializeField] private Image _buffIco;
        [SerializeField] private UnityEvent<float> _buffTimeChangeEvent;

        private TimedBuffInstance _timedBuffInstance;

        public TimedBuffInstance TimedBuffInstance => _timedBuffInstance;

        public delegate void BuffEnd(TimedBuffInstance buffInstance);
        public event BuffEnd BuffEndEvent;

        public void Setup(TimedBuffInstance timedBuffInstance)
        {
            _buffIco.sprite = timedBuffInstance.Buff.Ico;
            timedBuffInstance.TimerChaneEvent += UpdateLeftTime;
            ShowBuffDisplay();
        }

        private void UpdateLeftTime(float value)
        {
            if (Math.Abs(value - 1) < 0.1)
            {
                BuffEndEvent?.Invoke(_timedBuffInstance);
                HideBuffDisplay();
            }
            
            _buffTimeChangeEvent.Invoke(value);
        }

        private void ShowBuffDisplay()
        {
            gameObject.SetActive(true);
        }

        private void HideBuffDisplay()
        {
            gameObject.SetActive(false);
        }
    }
}