using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private FadeScreen fade;

    private void Start()
    {
        fade = GameObject.Find("__ Fade __").GetComponent<FadeScreen>();
    }
    public void StartGame()
    {
        // Initializing a level's data by creating the 'level' object itself.
        GameData.Begin();

        // Moves the scene to the next scene in the build index.
        fade.LoadNextLevel();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        // Simple method to 'quit' the game.
        Application.Quit();
    }
}
