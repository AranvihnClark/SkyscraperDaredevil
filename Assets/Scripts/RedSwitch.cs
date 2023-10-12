using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedSwitch : MonoBehaviour
{
    private bool canFlip;
    private float timeLeft = 0;
    private Animator flipSwitch;
    public bool isFlipped = false;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource switchSE;
    [SerializeField] private AudioSource electricityOffSE;

    private void Start()
    {
        canFlip = false;
        flipSwitch = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }
        if (timeLeft == 0)
        {
            if (canFlip && !isFlipped && player.GetComponent<PlayerMovement>().Interact())
            {
                switchSE.Play();
                flipSwitch.SetBool("flip", true);
                isFlipped = true;
                electricityOffSE.Play();
                timeLeft = 1f;
            }
            else if (canFlip && isFlipped && player.GetComponent<PlayerMovement>().Interact())
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
        if (collision.CompareTag("Player"))
        {
            canFlip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canFlip = false;
        }
    }
}
