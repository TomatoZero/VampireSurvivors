using UnityEngine;

namespace Player
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _hurtClip;
        [SerializeField] private AudioClip _levelUpClip;

        public void HurtEventHandler()
        {
            _audioSource.PlayOneShot(_hurtClip);
        }

        public void LevelUpEventHandler()
        {
            _audioSource.PlayOneShot(_levelUpClip);
        }
    }
}