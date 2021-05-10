using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // A function to simply load up the scene '1'.
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    // A function to shut down the application.
    public void ExitGame()
    {
        Application.Quit();
    }
}
