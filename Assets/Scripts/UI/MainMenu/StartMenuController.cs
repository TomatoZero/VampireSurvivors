using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class StartMenuController : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Level1");
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}