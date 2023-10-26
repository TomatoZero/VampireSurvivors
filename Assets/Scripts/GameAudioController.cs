using UnityEngine;

namespace DefaultNamespace
{
    public class GameAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _gameOverClip;
        [SerializeField] private AudioClip _battleClip;

        public void Battle()
        {
            _audioSource.Stop();
            _audioSource.clip = _battleClip;
            _audioSource.Play();
        }

        public void GameOver()
        {
            _audioSource.Stop();
            _audioSource.clip = _gameOverClip;
            _audioSource.Play();
        }
    }
}