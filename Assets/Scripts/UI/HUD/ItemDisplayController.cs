using System;
using Stats.Instances;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI.HUD
{
    public class ItemDisplayController : MonoBehaviour
    {
        [SerializeField] private Image _itemIco;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _amoCount;
        
        private WeaponReferences _references;

        public ObjectInstance Instance
        {
            get
            {
                if(_references is not null)
                    return _references.Instance;

                return null;
            }
        }

        private void OnEnable()
        {
            if (_references is not null)
                _references.AmountControl.AmoAmountUpdateEvent += UpdateAmoCount;
        }

        private void OnDisable()
        {
            if (_references is not null)
                _references.AmountControl.AmoAmountUpdateEvent -= UpdateAmoCount;
        }

        public void Set(WeaponReferences reference)
        {
            reference.AmountControl.AmoAmountUpdateEvent += UpdateAmoCount;
            _references = reference;
            UpdateAmoCount(reference.AmountControl.CurrentAmount);
            Set(reference.Instance);
        }
        
        public void Set(ObjectInstance instance)
        {
            _itemIco.sprite = instance.StatsData.Ico;
            _level.text = $"Level {instance.CurrentLvl}";
        }
        
        public void Setup(Sprite ico, int level)
        {
            _itemIco.sprite = ico;
            _level.text = $"Level {level}";
        }

        public void UpdateLevel(int level)
        {
            _level.text = $"Level {level}";
        }

        private void UpdateAmoCount(int count)
        {
            _amoCount.text = count.ToString();
        }
    }
}