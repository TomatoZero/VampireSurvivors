using UnityEngine;

namespace Player
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _hurtClip;
        [SerializeField] private AudioClip _levelUpClip;
        [SerializeField] private AudioClip _dieClip;

        public void HurtEventHandler()
        {
            _audioSource.PlayOneShot(_hurtClip);
        }

        public void LevelUpEventHandler()
        {
            _audioSource.PlayOneShot(_levelUpClip);
        }

        public void DieEventHandler()
        {
            _audioSource.PlayOneShot(_hurtClip);
        }
    }
}