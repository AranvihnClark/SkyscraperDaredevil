using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creates a private variable to store our player's 'info'.
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // We create a float variable that determines which direction our player is moving (for x axis).
    public float moveX = 0f;

    // Note, [SerializeField] allows use in Unity
    [SerializeField] public bool canMove;
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float jumpSpeed = 17f;
    [SerializeField] private LayerMask jumpableGround;

    // Creates a single variable to hold our player's movement state.
    private enum MovementState { idle, running, jumping, falling };

    // Player Audio Variables
    [SerializeField] private AudioSource jumpSE;

/********************************************************************************************/
/********************************************************************************************/
/********************************************************************************************/

    private void Start()
    {
        // This assigns our player's properties to our variable.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            // This will return either 1 or -1.
            // Note, we use Input.GetAxisRaw to grab the 'raw' data at the time of the update.
            moveX = Input.GetAxisRaw("Horizontal");

            // We then adjust our new velocity by multiplying it with a static value (walking speed).
            // The y value of our vector will remain the same as it currently was when this updated.
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

            // Then we also need to account for when the player jumps.
            // Note, like above with "Horizontal", we grab the "Jump" action.
            // These are determined in project settings under input management.
            if (Input.GetButtonDown("Jump") && OnGround())
            {
                jumpSE.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            UpdateAnimationState();
        }
    }

    public void Test()
    {

    }

    public void UpdateAnimationState() 
    {

        // This will determine our player's animation state.
        MovementState state;

        // Below is used to control our character's running state.
        //    0       1        2        3
        // { idle, running, jumping, falling }
        if (moveX != 0) 
        {
            state = MovementState.running;

            if (moveX > 0) {
                spriteRenderer.flipX = false;
            } else
            {
                spriteRenderer.flipX = true;
            }
        } else
        {
            state = MovementState.idle;
        }

        // We use a velocity of 0.01 because pixel movement isn't always exact and this ensures things work.
        if (rb.velocity.y > 0.01f) 
        {
            state = MovementState.jumping;
        } else if (rb.velocity.y < -0.01f)
        {
            state = MovementState.falling;
        }

        // Enum returns a number, but we still need to caste the Enum variable to read it as an int.
        animator.SetInteger("state", (int)state);
    }

    private bool OnGround()
    {
        // This creates a cast that covers what our player's box collider is.
        // It also moves the cast down just a smidge lower than our current box collider.
        // This allows us to check if the cast overlaps another box collider below our player only.
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.01f, jumpableGround);
    }
}
