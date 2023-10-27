using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.PlayerLoop;

public class FinishLine : MonoBehaviour
{
    // Serialize Fields
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueLevelMessage;
    [SerializeField] private TextMeshProUGUI dialogueListMessage;
    [SerializeField] private TextMeshProUGUI dialogueTimeMessage;
    [SerializeField] private TextMeshProUGUI dialogueItemsMessage;
    [SerializeField] private TextMeshProUGUI dialogueTotalMessage;
    [SerializeField] private string[] dialogue;
    [SerializeField] private float wordSpeed = 0.05f;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI timerText;

    // General variable declaration
    private bool isFinished = false;
    private int tempTotal;
    private float timeLeft = 60;

    // Finish Line Audio Variable
    private AudioSource finishLineSE;

    // Script Methods.
    private void Start()
    {
        // Initialization of our variable
        finishLineSE = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Keeping track of the timer.
        if (TimerSet.timerOn)
        {
            // If there is still time left, subtract our timer and update our message text.
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerText.text = string.Format("{0:00}", timeLeft);

            }
            else 
            {
                // If time is at 0, player dies.
                player.GetComponent<PlayerDeath>().Death();
                TimerSet.timerOn = false;
            }
        }

        // Checks to see if the text printed matches our saved text to show the 'continue' button.
        if (dialogueTotalMessage.text == dialogue[4])
        {
            continueButton.SetActive(true);
        }

        // I forget why this is here but I think there was an issue with the player updating when the game starts (since it technically falls when a scene starts).
        // May figure out a way to not have this here later.
        player.GetComponent<PlayerMovement>().UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This checks if the player is what triggered the box and if they haven't already somehow did so before already.
        if (collision.gameObject.name == "Player" && !isFinished)
        {
            // Pause the timer.
            TimerSet.timerOn = false;

            // Adjust our dialogue as below to match the player's current 'stats'.
            dialogue[2] = timerText.text;
            dialogue[3] = ItemCollector.skyTokens.ToString();

            // A temp variable declared above is used to store math so it doesn't look so ugly below.
            tempTotal = (int)Mathf.Round(timeLeft) + ItemCollector.skyTokens;

            dialogue[4] = tempTotal.ToString();

            // Then we create our next level object
            GameData.UpdateLevel(GameData.levels[GameData.currentLevel - 1], (int)Mathf.Round(timeLeft), ItemCollector.skyTokens, tempTotal, GameData.deaths);

            // Finish Line effects
            finishLineSE.Play();
            isFinished = true;
            
            // Restricting player movement for message display.
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0);
            player.GetComponent<PlayerMovement>().moveX = 0f;
            player.GetComponent<PlayerMovement>().canMove = false;
            
            // Activates message panel and disables continue button in case if it is active.
            dialoguePanel.SetActive(true);
            continueButton.SetActive(false);
            
            // Not sure if this helps but I will probably replace the above with this later.
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            // Starts coroutine for our typing method below.
            StartCoroutine(Typing());
        }
    }

    IEnumerator Typing()
    {
        // Types, in order of our dialogue, one character at a time.
        // Will probably add functionality to 'skip' through text.
        foreach (char letter in dialogue[0].ToCharArray())
        {
            dialogueLevelMessage.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        foreach (char letter in dialogue[1].ToCharArray())
        {
            dialogueListMessage.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        foreach (char letter in dialogue[2].ToCharArray())
        {
            dialogueTimeMessage.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        foreach (char letter in dialogue[3].ToCharArray())
        {
            dialogueItemsMessage.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        foreach (char letter in dialogue[4].ToCharArray())
        {
            dialogueTotalMessage.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Checks for a next line but in this case, there isn't really a need.
    // I should consider just removing this method and pushing it to the ResetText() below.
    public void NextLine()
    {
        continueButton.SetActive(false);
        ResetText();
    }

    // Self explantory method.
    private void ResetText()
    {
        dialogueLevelMessage.text = "";
        dialogueListMessage.text = "";
        dialogueTimeMessage.text = "";
        dialogueItemsMessage.text = "";
        dialogueTotalMessage.text = "";
        dialoguePanel.SetActive(false);
        Invoke("CompleteLevel", 0.1f);
    }

    // Resets global/player variables for the next scene.
    private void CompleteLevel()
    {
        player.GetComponent<PlayerMovement>().canMove = true;
        /* For checking if data was correctly saved
        for (int i = 0; i < GameData.levels.Count; i++)
        {
            Debug.Log(GameData.levels[i].ToString());
        }
        */
        GameData.NewLevel();
        ItemCollector.skyTokens = 0;
        ItemCollector.keyCollected = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
