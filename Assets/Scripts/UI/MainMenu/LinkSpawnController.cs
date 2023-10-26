using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class LinkSpawnController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _teleportParticle;
        
        public void StartAnimation()
        {
            Debug.Log("!!!!!!!");
            gameObject.SetActive(true);
        }
        
        public void OnAnimationEnd() 
        {
            _teleportParticle.Stop();
            SceneManager.LoadScene("Level1");
        }   
    }
}