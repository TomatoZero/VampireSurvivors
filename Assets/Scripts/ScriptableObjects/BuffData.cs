using Stats;
using Stats.Instances.Buff;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObject/Buff/Buff", order = 1)]
    public class BuffData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _ico;
        [SerializeField] private StatData _statData;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isDurationStacked;
        [SerializeField] private bool _isEffectStacked;


        public string Name => _name;
        public Sprite Ico => _ico;
        public StatData StatData => _statData;
        public float Duration => _duration;
        public bool IsDurationStacked => _isDurationStacked;
        public bool IsEffectStacked => _isEffectStacked;

        public TimedBuffInstance InitializeBuff()
        {
            return new TimedBuffInstance(this);
        }
    }
}