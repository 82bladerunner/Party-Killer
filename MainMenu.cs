using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene when the "Play" button is clicked
        SceneManager.LoadScene("GameScene");
    }

    public void OpenOptions()
    {
        // Implement code to open options menu (if applicable)
        Debug.Log("Options menu opened");
    }

    public void QuitGame()
    {
        // Quit the application when the "Quit" button is clicked
        Application.Quit();
    }
}
