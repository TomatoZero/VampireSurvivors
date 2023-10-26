using UnityEngine;

namespace Weapons
{
    public class WeaponAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _weaponHitClip;

        public void Hit()
        {
            _audioSource.PlayOneShot(_weaponHitClip);
        }
    }
}