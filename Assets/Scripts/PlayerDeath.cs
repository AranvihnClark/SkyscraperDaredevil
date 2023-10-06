using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D playerBody;

    // Death Audio Variable
    [SerializeField] private AudioSource deathSE;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GetComponent<PlayerMovement>().moveX = 0f;
            GetComponent<PlayerMovement>().canMove = false;
            deathSE.Play();
            Death();
        }
    }

    private void Death()
    {
        animator.SetTrigger("death");
        GameData.deaths++;
        GameData.totalDeaths++;
        playerBody.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        GetComponent<PlayerMovement>().canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
