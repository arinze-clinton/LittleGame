using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnStart()
    {
        // Load the first game scene...
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        // Quit the game...
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
