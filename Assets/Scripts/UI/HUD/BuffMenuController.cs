using System;
using System.Collections.Generic;
using Stats.Instances.Buff;
using UnityEngine;

namespace UI.HUD
{
    public class BuffMenuController : MonoBehaviour
    {
        [SerializeField] private List<BuffDisplayController> _currentDisplaysBuff;

        private Queue<BuffDisplayController> _freeDisplays;
        private Queue<TimedBuffInstance> _buffQueue;

        private void Awake()
        {
            _freeDisplays = new Queue<BuffDisplayController>();
            _buffQueue = new Queue<TimedBuffInstance>();
            foreach (var displayController in _currentDisplaysBuff)
            {
                _freeDisplays.Enqueue(displayController);
                displayController.BuffEndEvent += CloseBuff;
            }
        }

        public void ShowBuff(TimedBuffInstance buff)
        {
            if (_freeDisplays.TryDequeue(out BuffDisplayController buffDisplay))
            {
                buffDisplay.Setup(buff);
            }
            else
            {
                _buffQueue.Enqueue(buff);
            }
        }

        private void CloseBuff(TimedBuffInstance buffInstance)
        {
            foreach (var displayController in _currentDisplaysBuff)
            {
                if (displayController.TimedBuffInstance != buffInstance) continue;

                if (_buffQueue.TryDequeue(out TimedBuffInstance buff))
                    displayController.Setup(buff);
                else
                    _freeDisplays.Enqueue(displayController);

                return;
            }

            throw new Exception("You shouldn't be here");
        }
    }
}