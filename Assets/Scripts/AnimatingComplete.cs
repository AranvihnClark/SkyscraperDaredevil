using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatingComplete : MonoBehaviour
{
    [SerializeField] private bool isAnimating;
    [SerializeField] GameObject levelStart;
    private int timeCount;
    
    private void Start()
    {
        PlayerMovement.canMove = false;
        isAnimating = true;
        timeCount = 0;

        try
        {
            levelStart.SetActive(false);
        }
        catch {}
    }

    private void Update()
    {
        if (isAnimating)
        {
            TimerSet.timerOn = false;
        }
        if (!isAnimating)
        {
            GameData.isLoading = false;

            try
            {
                levelStart.SetActive(true);
            }
            catch 
            {
                if (timeCount == 0)
                {
                    StartTime();
                }
            };

        }
    }

    private void StartTime()
    {
        TimerSet.timerOn = true;
        timeCount++;
        PlayerMovement.canMove = true;
    }
}
