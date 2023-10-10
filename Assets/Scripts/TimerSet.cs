using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSet : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float timeLeft = 60;
    public static bool timerOn = false;

    // Start is called before the first frame update
    private void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else 
            {
                player.GetComponent<PlayerDeath>().Death();
                timerOn = false;
            }
        }
    }
}
