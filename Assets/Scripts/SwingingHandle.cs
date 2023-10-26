using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwingingHandle : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D playerRB;
    private Rigidbody2D chainRB;

    private bool isTouching;

    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        chainRB = GetComponent<Rigidbody2D>();
        isTouching = false;
    }
    private void Update()
    {
        if (isTouching)
        {
            if (Input.GetButton("Fire1"))
            {
                player.transform.position = new Vector2(transform.position.x, transform.position.y);
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
                if (playerRB.velocity.x > 0 || playerRB.velocity.x < 0)
                {
                    chainRB.velocity = new Vector2(playerRB.velocity.x * 1.5f, -10f);
                }
                else {}
            }
            if (Input.GetButtonUp("Fire1"))
            {
                // Needs to be fixed - velocity is a constant it seems and this takes the player's velocity of 12f.
                if (Mathf.Abs(chainRB.velocity.x) !> 2f)
                {
                    playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Abs(playerRB.velocity.x * 1.5f));
                }
                else {}
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }
}
