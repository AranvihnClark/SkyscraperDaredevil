using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSet : MonoBehaviour
{
    // Variable delcarations.
    [SerializeField] GameObject player;
    public float timeLeft;
    public static bool timerOn;

    // Start is called before the first frame update
    private void Start()
    {
        // Initializing our variables.
        timeLeft = 60;
        timerOn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerOn)
        {
            // If there is still time left, we want to reduce that time per frame per second.
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else 
            {
                // Otherwise, if time hits 0, the player loses and 'dies'.
                player.GetComponent<PlayerDeath>().Death();
                timerOn = false;
            }
        }
    }
}
