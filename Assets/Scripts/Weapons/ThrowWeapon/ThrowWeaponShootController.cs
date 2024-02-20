using Stats.Instances;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.ThrowWeapon
{
    public class ThrowWeaponShootController : WeaponTopDownShootController
    {
        [SerializeField] private AmoAmountControl _amoAmountControl;
        [SerializeField] private UnityEvent<Vector2> _hitPosition;

        protected AmoAmountControl AmoAmountControl => _amoAmountControl;
        protected UnityEvent<Vector2> HitPosition => _hitPosition;

        private int _baseAmount;
        private float _reloadTime;

        public override void ShootEventHandler(Vector2 mousePosition)
        {
            if (_amoAmountControl.IsEnoughAmo)
            {
                _amoAmountControl.TakeAmo();
                _hitPosition.Invoke(mousePosition);
            }
        }

        public virtual Vector2 GetFallPosition(Vector2 mousePosition)
        {
            return transform.InverseTransformPoint(mousePosition);
        }

        public override void SetupStatEventHandler(ObjectInstance newInstance)
        {
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _reloadTime = newInstance.GetStatByName(Stats.Stats.Reload).Value;
            
            _amoAmountControl.SetAmoData(_baseAmount, _reloadTime);
        }

        public override void UpdateStatsEventHandler(ObjectInstance newInstance)
        {
            _baseAmount = (int)newInstance.GetStatByName(Stats.Stats.Amount).Value;
            _reloadTime = newInstance.GetStatByName(Stats.Stats.Reload).Value;
            
            _amoAmountControl.SetAmoData(_baseAmount, _reloadTime);
        }
    }
}