using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScreen : MonoBehaviour
{
    // Variable declaration
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    // Public method to be called by other scripts (mainly for the 'GameMenu' and the 'FinishLine' scripts)
    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        // Starts a coroutine of the below IEnumerator method.
        // This essentially lets us play the fade out animation, wait, change scene, and then fade in animation.

        // **** The 'buildIndex + 1' will definitely have to be changed to account for manual scene changing.
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play Animation based on our Animator's trigger.
        transition.SetTrigger("FadeOut");

        // Wait based on our variable time.
        yield return new WaitForSeconds(transitionTime);

        // Load's the next scene.
        SceneManager.LoadScene(levelIndex);
    }
}
