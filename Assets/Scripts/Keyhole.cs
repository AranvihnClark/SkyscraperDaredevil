using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    // Variable declarations.
    [SerializeField] private GameObject keysInHand;
    [SerializeField] private Sprite image;
    [SerializeField] private AudioSource unlockSE;

    private bool canUseKey;

    public bool isUnlocked;
    
    private Animator animator;

    private void Start()
    {
        // Initialization of our variables.
        animator = GetComponent<Animator>();
        canUseKey = false;
        isUnlocked = false;
    }

    private void Update()
    {
        // Checks to see if a key was collected this scene.
        if (ItemCollector.keyCollected)
        {
            // Update our screen to show the image of the key collected.
            keysInHand.GetComponent<UnityEngine.UI.Image>().sprite = image;
            keysInHand.SetActive(true);
        }

        if (!ItemCollector.keyCollected)
        {
            // If the key is no longer in our possesion, ie used, we remove the pic.
            keysInHand.SetActive(false);
        }

        // Checks to see if we have the key and interacted with the keyhole.
        if (canUseKey && Input.GetButtonDown("Fire1"))
        {
            unlockSE.Play();
            if (!isUnlocked)
            {
                // If we inserted the key, we want to change the animation to change the sprite (that's just how I did it).
                // And then we turn the ItemCollecter variable to false to remove the pic in the corner of our key.
                animator.SetBool("isUnlocked", true);
                isUnlocked = true;
                ItemCollector.keyCollected = false;
            }
            else if (isUnlocked)
            {
                // If the player wishes, they can take the key back to be used elsewhere.
                // This is in preparation for more complex maps, but it may not be used. We'll see.
                animator.SetBool("isUnlocked", false);
                isUnlocked = false;
                ItemCollector.keyCollected = true;
            }
        }
    }

    // Checks to see if the player is next to the keyhole or not.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canUseKey = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canUseKey = false;
        }
    }
}
