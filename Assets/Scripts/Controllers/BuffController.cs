using System;
using System.Collections.Generic;
using ScriptableObjects;
using Stats.Instances.Buff;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class BuffController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<TimedBuffInstance> _buffTimerChangeEvent;

        [SerializeField]
        private UnityEvent<Dictionary<Stats.Stats, float>, Dictionary<Stats.Stats, float>> _buffChangeEvent;

        private Dictionary<BuffData, TimedBuffInstance> _buffs;
        private Dictionary<Stats.Stats, float> _clearBuff;
        private Dictionary<Stats.Stats, float> _percentBuff;

        private void Awake()
        {
            _buffs = new Dictionary<BuffData, TimedBuffInstance>();
            _clearBuff = new Dictionary<Stats.Stats, float>();
            _percentBuff = new Dictionary<Stats.Stats, float>();
        }

        public TimedBuffInstance AddBuff(BuffData buff)
        {
            if (_buffs.ContainsKey(buff))
            {
                _buffs[buff].Activate();
                AddBuffStat(buff);
                return _buffs[buff];
            }
            else
            {
                var buffInstance = buff.InitializeBuff();
                _buffs.Add(buff, buffInstance);
                AddBuffStat(buff);
                ShowNewBuff(buffInstance);
                buffInstance.RemoveBuff += RemoveBuff;
                
                _buffs[buff].Activate();

                StartCoroutine(_buffs[buff].StartCountdown());
                return _buffs[buff];
            }
        }

        private void ShowNewBuff(TimedBuffInstance instance)
        {
            _buffTimerChangeEvent.Invoke(instance);
        }

        private void AddBuffStat(BuffData buffData)
        {
            if (buffData.StatData.IsPercent) AddValueInDictionary(_percentBuff, buffData);
            else AddValueInDictionary(_clearBuff, buffData);

            _buffChangeEvent.Invoke(_clearBuff, _percentBuff);
        }

        private void AddValueInDictionary(Dictionary<Stats.Stats, float> dictionary, BuffData buffData)
        {
            if (dictionary.ContainsKey(buffData.StatData.Stat))
            {
                if (buffData.IsEffectStacked)
                    dictionary[buffData.StatData.Stat] += buffData.StatData.Value;
            }
            else
            {
                dictionary.Add(buffData.StatData.Stat, buffData.StatData.Value);
            }
        }
        
        private void RemoveBuff(TimedBuffInstance buff)
        {
            if (!buff.Buff.StatData.IsPercent)
            {
                if (_clearBuff.ContainsKey(buff.Buff.StatData.Stat))
                    RemoveValueFromDictionary(_clearBuff, buff.Buff);
                else
                    throw new Exception("Stat was not found");
            }
            else
            {
                if (_percentBuff.ContainsKey(buff.Buff.StatData.Stat))
                    RemoveValueFromDictionary(_percentBuff, buff.Buff);
                else
                    throw new Exception("Stat was not found");
            }

            if (_buffs.ContainsKey(buff.Buff))
                _buffs.Remove(buff.Buff);
            
            _buffChangeEvent.Invoke(_clearBuff, _percentBuff);
        }

        private void RemoveValueFromDictionary(Dictionary<Stats.Stats, float> dictionary, BuffData buffData)
        {
            dictionary[buffData.StatData.Stat] -= buffData.StatData.Value;

            if (dictionary[buffData.StatData.Stat] == 0)
                dictionary.Remove(buffData.StatData.Stat);
        }
        
        private void PrintDictionary()
        {
            var str = "PlayerBffController:\n\n";
            str += "ClearBuff:\n";

            foreach (var buff in _clearBuff)
            {
                str += $"Stat: {buff.Key}, Value: {buff.Value}\n";
            }

            str += "\nPercentBuff: \n";
            
            foreach (var buff in _percentBuff)
            {
                str += $"Stat: {buff.Key}, Value: {buff.Value}\n";
            }
            
            Debug.Log(str);
        }
    }
}