using Interface;
using Stats.ScriptableObjects;
using UnityEngine;

namespace Stats.Instances.Buff
{
    public abstract class TimedBuffInstance
    {
        private readonly BuffData _buff;
        
        private float _duration;
        private int _effectStacks;
        private readonly ICanGetBuff _buffClass;
        private bool _isFinished;
        

        public TimedBuffInstance(BuffData buff, ICanGetBuff buffClass)
        {
            _buff = buff;
            _buffClass = buffClass;
            _isFinished = false;
        }

        public void Tick(float delta)
        {
            _duration -= delta;
            if (_duration <= 0)
            {
                End();
                _isFinished = true;
            }
        }

        public void Activate()
        {
            if (_buff.IsEffectStacked || _duration <= 0)
            {
                ApplyEffect();
                _effectStacks++;
            }
        
            if (_buff.IsDurationStacked || _duration <= 0)
            {
                _duration += _buff.Duration;
            }
        }
        
        protected abstract void ApplyEffect();
        public abstract void End();
    }
}