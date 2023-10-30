using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Initializing a level's data by creating the 'level' object itself.
        GameData.Begin();

        // Moves the scene to the next scene in the build index.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu()
    {

    }

    public void Credits()
    {
        
    }

    public void Exit()
    {
        // Simple method to 'quit' the game.
        Application.Quit();
    }
}
