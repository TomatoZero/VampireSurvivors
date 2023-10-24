using UnityEngine;

namespace UI
{
    public class UISoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Button Sounds")] [SerializeField]
        private AudioClip _buttonHoverClip;

        [SerializeField] private AudioClip _buttonClickClip;

        public void ButtonHover()
        {
            _audioSource.PlayOneShot(_buttonHoverClip);
        }

        public void ButtonClick()
        {
            _audioSource.PlayOneShot(_buttonClickClip);
        }
    }
}