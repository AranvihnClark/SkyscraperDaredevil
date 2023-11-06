using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwingingHandle : MonoBehaviour
{
    // Variable declarations.
    // A player SerializeField is needed so that the player can 'interact' with the chain directly.
    [SerializeField] private GameObject player;
    private float objectSpeed;
    private Rigidbody2D playerRB;
    private Rigidbody2D chainRB;
    private bool isTouching;
    private float x1;
    private float x2;
    private float originalX;

    private void Start()
    {
        // Just assiging variables at the start of the scene.
        playerRB = player.GetComponent<Rigidbody2D>();
        chainRB = GetComponent<Rigidbody2D>();
        isTouching = false;

        // Making sure that we keep the original x position of our chain to 'properly' track the swing
        originalX = transform.position.x;
        x1 = originalX;
    }

    private void Update()
    {
        // We make x2 = to x1 and then check to see if the chain's 'x' position has changed.
        // If it has, we return the new value - otherwise it retains the old value and no update happens.
        x2 = x1;
        x1 = CheckX(transform.position.x);

        // If the two 'x' values have in fact changed, we calculate the object's speed.
        // I did it this way as I was having trouble finding a value of speed from Unity itself.
        if (x1 != x2)
        {
            objectSpeed = Mathf.Abs(x1-x2) / Time.deltaTime;
        }
        
        // Now, we confirm if our player is touching the trigger box.
        if (isTouching)
        {
            // If the player is within the trigger box, we check to see if the action, or 'fire1' button has been pressed and held down.
            if (Input.GetButton("Fire1"))
            {
                // Below is to keep the player 'attached' to the chain as it moves.
                player.transform.position = new Vector2(transform.position.x, transform.position.y);

                // This is needed because if the player lets go, the 'y' velocity will continue to add up and cause the player to 'fall' super fast and glitch through collider boxes.
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
                
                // If the player goes left or right (essentially), we want to update the chain's velocity.
                if (playerRB.velocity.x > 0f || playerRB.velocity.x < 0f)
                {
                    playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
                    chainRB.velocity = new Vector2(playerRB.velocity.x * 1.5f, -10f);
                }
                else {}
            }

            // When the play lets go of the action button to 'hold' to the chain, the below happens.
            if (Input.GetButtonUp("Fire1"))
            {
                // If the aboslute value of our chain's speed reaches a certain threshold, the below happens.
                if (Mathf.Abs(objectSpeed) >= 15f)
                {
                    // Two if statements are used as the below parameters can't be put above together.
                    // This checks to make sure the chain has gone a certain distance to allow the player to emulate 'jumping/flying' off the chain.
                    if (transform.position.x > (originalX + 1f) || transform.position.x < (originalX - 1f))
                    {
                        playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Abs(playerRB.velocity.x * 1.5f));
                    }
                }
                else {}
            }
        }
    }

    // Used to check if the game has correctly detected a change in the chain's 'x' position.
    // This way, our calculaton doesn't return a 0 and cause an issue later (hopefully).
    private float CheckX(float x)
    {
        if (x != x2)
        {
            return x;
        }
        else { return x2; }
    }

    // Just a trigger entry method that checks off if our player is touching the box.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
    }

    // A trigger exit method to lets our script know our player is no longer touching it.
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }
}
