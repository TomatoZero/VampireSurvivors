using System;
using UnityEngine;

namespace UI
{
    public class UISoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Button Sounds")] [SerializeField] private AudioClip _buttonHoverClip;
        [SerializeField] private AudioClip _buttonClickClip;

        [Header("Pause menu sounds")] [SerializeField] private AudioClip _turnOnPauseClip;
        [SerializeField] private AudioClip _turnOffPauseClip;

        private void Awake()
        {
            //Load clip from streaming asset
            //Better use asset bundle
        }

        public void ButtonHover()
        {
            _audioSource.PlayOneShot(_buttonHoverClip);
        }

        public void ButtonClick()
        {
            _audioSource.PlayOneShot(_buttonClickClip);
        }

        public void TurnOnPause()
        {
            _audioSource.PlayOneShot(_turnOnPauseClip);
        }
        
        public void TurnOffPause()
        {
            _audioSource.PlayOneShot(_turnOffPauseClip);
        }
    }
}