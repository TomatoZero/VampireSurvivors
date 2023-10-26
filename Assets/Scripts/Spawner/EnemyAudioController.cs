using UnityEngine;

namespace Spawner
{
    public class EnemyAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _enemyDieClip;

        public void EnemyDieEventHandler()
        {
            _audioSource.PlayOneShot(_enemyDieClip);
        }
    }
}