using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedSwitch : MonoBehaviour
{
    // Variable declarations.
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource switchSE;
    [SerializeField] private AudioSource electricityOffSE;
    private bool canFlip;
    private Animator flipSwitch;
    public bool isFlipped = false;
    
    // The specific variable below is used to 'prevent' multiple uses of our switch in a row.
    private float timeLeft;

    private void Start()
    {
        // Initialization of our variables.
        canFlip = false;
        flipSwitch = gameObject.GetComponent<Animator>();

        // This is initialized to 0 because when the scene starts, you can (in theory), press the switch right away.
        timeLeft = 0;
    }

    private void Update()
    {
        // First we subtract what time we have left with the time that has passed during a frame.
        timeLeft -= Time.deltaTime;

        // If that time is less than 0 because of the above, we reassign time to 0.
        // Just in case if the time gets messed up and becomes a super high negative number by mistake.
        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }

        // If timeLeft is = 0, we want to allow our player to flip the switch, as below.
        if (timeLeft == 0)
        {
            // Before we can 'flip' the switch, we check player's box collider is in the trigger area,
            // which way the switch is looking, and if the player is pressing the 'fire1' button.
            if (canFlip && !isFlipped && Input.GetButtonDown("Fire1"))
            {
                switchSE.Play();
                flipSwitch.SetBool("flip", true);
                isFlipped = true;
                electricityOffSE.Play();

                // timeLeft is given a second to wait before the player can flip the switch again.
                timeLeft = 1f;
            }

            // The below happens instead of it was already previously flipped.
            // This is needed essentially to confirm if the electricity is turned off or turned back on.
            // Probably necessary later for more complex levels if I get that far.
            else if (canFlip && isFlipped && Input.GetButtonDown("Fire1"))
            {
                switchSE.Play();
                flipSwitch.SetBool("flip", false);
                isFlipped = false;
                electricityOffSE.Play();
                timeLeft = 1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the player is in the trigger box.
        if (collision.CompareTag("Player"))
        {
            canFlip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Changes the boolean below to false if the player leaves.
        if (collision.CompareTag("Player"))
        {
            canFlip = false;
        }
    }
}
