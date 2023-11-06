using UnityEngine;

public class AnimatingComplete : MonoBehaviour
{
    [SerializeField] private bool isAnimating;
    [SerializeField] GameObject levelStart;
    [SerializeField] Rigidbody2D player;
    private int timeCount;
    
    private void Start()
    {
        try
        {
            player.GetComponent<PlayerMovement>().canMove = false;
        }
        catch {}

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
        try
        {
            player.GetComponent<PlayerMovement>().canMove = true;
        }
        catch {}
    }
}
