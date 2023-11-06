using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // Variable declations
    private Animator animator;
    private Rigidbody2D playerBody;

    // Death Audio Variable
    [SerializeField] private AudioSource deathSE;

    private void Start()
    {
        // Initialization of our variables.
        animator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with a game object's box collider with a the tag below...
        if (collision.gameObject.CompareTag("Traps"))
        {
            // First we prevent movement.
            // I also adjust the player's velocity to remove the warning unity gives us for attempting to have a velocity for a static object.
            PlayerMovement.canMove = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            GetComponent<PlayerMovement>().moveX = 0f;

            // Then we play out our death sequence.
            Death();
        }
    }

    public void Death()
    {
        deathSE.Play();

        // We used the animator with a trigger to start the animation for our character's death.
        animator.SetTrigger("death");

        // We update global data below.
        GameData.deaths++;
        GameData.totalDeaths++;
        ItemCollector.skyTokens = 0;

        // We make the player's rigid body static to prevent it from 'falling' and having our camera follow it.
        playerBody.bodyType = RigidbodyType2D.Static;
    }

    // Reinitializes level variables to restart.
    // I believe this is being handled as an Animation Event so there is technically a reference.
    private void RestartLevel()
    {
        PlayerMovement.canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
