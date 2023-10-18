using System.Collections;
using Stats.ScriptableObjects;
using UnityEngine;

namespace Stats.Instances.Buff
{
    public class TimedBuffInstance
    {
        private readonly BuffData _buff;

        private float _duration;
        private float _currentDuration;
        private int _effectStacks;

        public BuffData Buff => _buff;
        public float Duration => _duration;
        public float CurrentDuration => _currentDuration;
        public int EffectStacks => _effectStacks;

        public delegate void TimerChane(TimedBuffInstance instance);

        public event TimerChane TimerChaneEvent;


        public TimedBuffInstance(BuffData buff)
        {
            _buff = buff;
        }

        public IEnumerator StartCountdown()
        {
            while (true)
            {
                yield return new WaitForSeconds(.1f);
                _currentDuration += .1f;
                TimerChaneEvent?.Invoke(this);

                if (_currentDuration >= _duration)
                    break;
            }
        }

        public void Activate()
        {
            if (_buff.IsEffectStacked || _currentDuration <= 0)
            {
                _effectStacks++;
            }

            if (_buff.IsDurationStacked || _currentDuration <= 0)
            {
                _duration += _buff.Duration;
            }
            else
            {
                _currentDuration = 0;
            }
        }
    }
}