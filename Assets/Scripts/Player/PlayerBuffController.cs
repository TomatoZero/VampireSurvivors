using System;
using System.Collections.Generic;
using Stats;
using Stats.Instances.Buff;
using Stats.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerBuffController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<TimedBuffInstance> _buffTimerChangeEvent;
        [SerializeField] private UnityEvent<Dictionary<Stats.Stats, float>> _buffChangeEvent;
        
        private Dictionary<BuffData, TimedBuffInstance> _buffs;
        private Dictionary<Stats.Stats, float> _currentBuff;

        private void Awake()
        {
            _buffs = new Dictionary<BuffData, TimedBuffInstance>();
            _currentBuff = new Dictionary<Stats.Stats, float>();
        }

        public void AddBuff(BuffData buff)
        {
            if (_buffs.ContainsKey(buff))
            {
                _buffs[buff].Activate();
                AddBuffStat(buff);
            }
            else
            {
                var buffInstance = buff.InitializeBuff();
                _buffs.Add(buff, buffInstance);
                AddBuffStat(buff);
                
                _buffs[buff].Activate();
                _buffs[buff].TimerChaneEvent += TimerChaneEventHandler;
                
                StartCoroutine(_buffs[buff].StartCountdown());
            }
        }

        private void TimerChaneEventHandler(TimedBuffInstance instance)
        {
            _buffTimerChangeEvent.Invoke(instance);

            if (instance.CurrentDuration >= instance.Duration)
            {
                _buffs.Remove(instance.Buff);
                RemoveBuffStat(instance.Buff);
            }
        }
        
        private void AddBuffStat(BuffData buffData)
        {
            if (_currentBuff.ContainsKey(buffData.StatData.Stat))
            {
                if(buffData.IsEffectStacked)
                    _currentBuff[buffData.StatData.Stat] += buffData.StatData.Value;
            }
            else
            {
                _currentBuff.Add(buffData.StatData.Stat, buffData.StatData.Value);
            }
            
            _buffChangeEvent.Invoke(_currentBuff);
        }

        private void RemoveBuffStat(BuffData buff)
        {
            if (!_currentBuff.ContainsKey(buff.StatData.Stat))
            {
                return;
            }
            
            _currentBuff[buff.StatData.Stat] -= buff.StatData.Value;
            _buffChangeEvent.Invoke(_currentBuff);
        }
    }
}