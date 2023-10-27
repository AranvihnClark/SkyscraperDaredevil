using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Declaring our variables.
    private SpriteRenderer sprite;
    private Rigidbody2D rg;
    private bool isCollided = false;
    public bool isTriggered = false;
    
    // The below variable keeps track of the 'time' the sprite has before it starts 'falling'
    private float alphaTime = 0.8f;

    private void Start()
     {
        // Initializing our variables.
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rg = gameObject.GetComponent<Rigidbody2D>();
     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks to see if it was a player that entered onto the falling platform.
        // If so, a boolean is checked true so our update method below can begin.
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    private void Update()
    {
        if (isTriggered)
        {
            // If there is still time on our variable, we subtract per frame until it is less than or equal to 0.
            if (alphaTime > 0f)
            {
                alphaTime -= Time.deltaTime;
            }
            else 
            {
                // When no time is left, we change the platform's rigid body type and change the gravity scale to affect fall speed.
                // We also freeze the rotation aspect of the sprite as I don't need it spinning when it collides to something.
                rg.bodyType = RigidbodyType2D.Dynamic;
                rg.gravityScale = 5f;
                rg.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if (isTriggered && isCollided)
        {
            sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (2.5f * Time.deltaTime));
            if (sprite.color.a <= 0f)
            {
                // Basically a destroy method.
                // This probably isn't needed, but I was testing stuff and just left it as is.
                // May change later.
                PlatformVanish();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // This is used to check to see when the platform hits another platform or the street to 'disappear'.
        // We don't want it to do anything if it somehow detects the player or a rope.
        // Let's face it, a steel beam isn't going to be kept up if it falls onto a single line of rope.
        if (collision.gameObject.CompareTag("Player")) {}
        else if (collision.gameObject.CompareTag("FallThrough")) {}
        else
        {
            isCollided = true;
        }
    }

    private void PlatformVanish()
    {
        Destroy(gameObject);
    }

}
