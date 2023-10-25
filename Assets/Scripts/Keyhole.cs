using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject keysInHand;
    [SerializeField] private Sprite image;

    private bool canUseKey;

    public bool isUnlocked;
    
    private Animator animator;
    [SerializeField] private AudioSource unlockSE;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isUnlocked = false;
    }

    private void Update()
    {
        if (ItemCollector.keyCollected)
        {
            keysInHand.GetComponent<UnityEngine.UI.Image>().sprite = image;
            keysInHand.SetActive(true);
        }

        if (!ItemCollector.keyCollected)
        {
            keysInHand.SetActive(false);
        }

        if (canUseKey && player.GetComponent<PlayerMovement>().Interact())
        {
            unlockSE.Play();
            if (!isUnlocked)
            {
                animator.SetBool("isUnlocked", true);
                isUnlocked = true;
                ItemCollector.keyCollected = false;
            }
            else if (isUnlocked)
            {
                animator.SetBool("isUnlocked", false);
                isUnlocked = false;
                ItemCollector.keyCollected = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canUseKey = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canUseKey = false;
    }
}
