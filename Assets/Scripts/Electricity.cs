using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    // Declaring variables.
    // The SerializeField variable is named as such because it could be a Switch or a KeyHole.
    [SerializeField] private GameObject redConditional;
    private BoxCollider2D bc2D;
    private Animator electricityOn;

    private void Start()
    {
        // Initializing our variables at the start of the scene.
        electricityOn = gameObject.GetComponent<Animator>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // We use a try/catch here to check if we are dealing with a swtich or a keyhole.
        try
        {
            // If a swtich and the user has interacted with it, the electricity is turned 'off'.
            if (redConditional.GetComponent<RedSwitch>().isFlipped)
            {
                electricityOn.SetBool("Off", true);
                bc2D.enabled = false;
            }
            if (!redConditional.GetComponent<RedSwitch>().isFlipped)
            {
                electricityOn.SetBool("Off", false);
                bc2D.enabled = true;
            }
        }
        catch {}

        try
        {
            // For a keyhole - basically the same as the above.
            if (redConditional.GetComponent<Keyhole>().isUnlocked)
            {
                electricityOn.SetBool("Off", true);
                bc2D.enabled = false;
            }
            if (!redConditional.GetComponent<Keyhole>().isUnlocked)
            {
                electricityOn.SetBool("Off", false);
                bc2D.enabled = true;
            }
        }
        catch {}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks to see if the player is what 'entered' the electricity to 'kill' them.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDeath>().Death();
        }
    }
}
