using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _teleportClip;

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

        public void Teleport()
        {
            _audioSource.Pause();
            _audioSource.clip = _teleportClip;
            _audioSource.PlayDelayed(.1f);
        }
    }
}