using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads a scene by its name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        // Reset time scale in case the game was paused
        Debug.Log("Button clicked! Scene to load: " + sceneName);
        Time.timeScale = 1;
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
