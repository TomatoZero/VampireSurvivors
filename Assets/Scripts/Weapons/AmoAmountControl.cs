using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class AmoAmountControl : MonoBehaviour  //Change name. to something what using for count amo and how many hit can do (melee)
    {
        public event Action<int> AmoAmountUpdateEvent;

        private int _baseAmount;
        private int _currentAmount;
        private int _unActiveAmo;

        private float _amoRestoreTime;
        private float _amoRestoreTimePassed;

        private Coroutine _restoreAmoCoroutine;

        public int CurrentAmount => _currentAmount;
        public bool IsEnoughAmo => _currentAmount > 0;


        public void SetAmoData(int amo, float amoRestoreTime)
        {
            _baseAmount = amo;
            _amoRestoreTime = amoRestoreTime;

            if (_currentAmount < _baseAmount)
            {
                _currentAmount++;
                AmoAmountUpdateEvent?.Invoke(_currentAmount);
            }
        }

        public void TakeAmo()
        {
            if (!IsEnoughAmo) return;

            _currentAmount--;
            AmoAmountUpdateEvent?.Invoke(_currentAmount);
            TryStartRestoreProcess();
        }

        private void TryStartRestoreProcess()
        {
            if (_restoreAmoCoroutine is not null)
                StopCoroutine(_restoreAmoCoroutine);

            _restoreAmoCoroutine = StartCoroutine(RestoreAmo());
        }

        private IEnumerator RestoreAmo()
        {
            while (true)
            {
                _amoRestoreTimePassed += .1f;

                if (_amoRestoreTimePassed >= _amoRestoreTime)
                {
                    _currentAmount++;
                    _amoRestoreTimePassed = 0f;
                    AmoAmountUpdateEvent?.Invoke(_currentAmount);
                }

                if (_currentAmount >= _baseAmount)
                {
                    _amoRestoreTimePassed = 0f;
                    break;
                }

                yield return new WaitForSeconds(.1f);
            }
        }
    }
}