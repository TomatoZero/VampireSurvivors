﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string> _updatePassedTime;

        private WaitForSeconds _second;
        private int _passedSeconds;

        private void Awake()
        {
            _second = new WaitForSeconds(1f);
        }

        private void Start()
        {
            StartCoroutine(Timer());
        }

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void TurnOnTime()
        {
            if (Time.timeScale == 1)
            {
                Debug.LogWarning("TimeScale already set to 1");
                return;
            }

            Time.timeScale = 1;
        }

        public void TurnOffTime()
        {
            if (Time.timeScale == 0)
            {
                Debug.LogWarning("TimeScale already set to 0");
                return;
            }

            Time.timeScale = 0;
        }

        public void TurnTime()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                yield return _second;
                _passedSeconds++;
                
                var minutes = _passedSeconds / 60;
                var seconds = _passedSeconds % 60;

                var formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
                
                _updatePassedTime.Invoke(formattedTime);
            }
        }

        private void OnSceneUnloaded(Scene unloadedScene)
        {
            TurnOnTime();
        }
    }
}