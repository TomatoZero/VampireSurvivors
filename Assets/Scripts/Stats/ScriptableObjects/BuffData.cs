using Stats.Instances.Buff;
using UnityEngine;

namespace Stats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObject/Buff/Buff", order = 1)]
    public abstract class BuffData : ScriptableObject
    {
        [SerializeField] private float _duration;
        [SerializeField] private bool _isDurationStacked;
        [SerializeField] private bool _isEffectStacked;

        public float Duration => _duration;
        public bool IsDurationStacked => _isDurationStacked;
        public bool IsEffectStacked => _isEffectStacked;

        public abstract TimedBuffInstance InitializeBuff(GameObject obj);
    }
}