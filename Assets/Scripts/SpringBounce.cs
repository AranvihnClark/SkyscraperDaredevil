using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

public class SpringBounce : MonoBehaviour
{
    // Variable declarations.
    private Animator animator;
    private float modifier;
    private bool isBouncing;
    
    // The below variable is an array since our spring object has more than one box collider (a trigger and 2 normal collider)
    // The second collider may be removed later.
    // I have two because I think it is necessary for when I shrink one of the colliders to emulate a player bouncing on it.
    private BoxCollider2D[] bc2D;

    private void Start()
    {
        // Initializing our variables.
        animator = GetComponent<Animator>();
        bc2D = GetComponents<BoxCollider2D>();
        modifier = 0.11f;
        isBouncing = false;
    }
    
    private void Update()
    {
        // Checks to see if the player has touched the trigger box.
        if (isBouncing)
        {
            // Once the player triggers is, they player is already 'frozen' as handled by the PlayerMovement script.
            // The below will reduce the collider box's size
            // In essence, this lowers the player down as the animation lowers the spring.
            bc2D[1].size = new UnityEngine.Vector2(bc2D[1].size.x, bc2D[1].size.y - modifier);

            // Bceause this is being handled in the 'Update' method, we need to check if the size reaches 0.
            // If it does, we convert the modifier to emulate the spring coming back up.
            if (bc2D[1].size.y <= 0.01)
            {
                modifier = -modifier;
            }

            // Once the spring box collider has reached back to where it once was, we emulate the spring bounce.
            if (isBouncing && bc2D[1].size.y > 0.85f)
            {
                // We turn change boolean to false as we only need this to happen once per trigger.
                // I also have it in the trigger exit method below in case.
                isBouncing = false;

                // This is to revert the box collider to its original values in case it doesn't.
                bc2D[1].size = new UnityEngine.Vector2(bc2D[1].size.x, 0.85f);

                // Then we change our modifier back to the original positive value.
                modifier = -modifier;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // We activate the trigger to start the spring animation of 'bouncing'.
        animator.SetTrigger("onTouch");

        // The boolean below lets our update method know a player is touching the spring's trigger box.
        isBouncing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Really just here as a backup.
        isBouncing = false;
    }
}
