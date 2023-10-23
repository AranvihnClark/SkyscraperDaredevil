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
            Death();
        }
    }

    public void Death()
    {
        deathSE.Play();
        animator.SetTrigger("death");
        GameData.deaths++;
        GameData.totalDeaths++;
        ItemCollector.skyTokens = 0;
        playerBody.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        GetComponent<PlayerMovement>().canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
