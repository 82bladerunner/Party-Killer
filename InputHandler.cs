using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    void Update()
    {
        // Check for input every frame

        // Check if ESC key is pressed to close the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseGame();
        }

        // Check if R key is pressed to reload the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadFirstScene();
        }
    }

    void CloseGame()
    {
        // Close the game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void ReloadFirstScene()
    {
        // Reload the first scene
        SceneManager.LoadScene(0);
    }
}
