using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TutorialMessage : MonoBehaviour
{
    // Variable declarations.
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogue;
    [SerializeField] private float wordSpeed = 0.05f;
    [SerializeField] private GameObject continueButton;

    // Not necessary to 'initialize' as it seems it starts as 0 if it isn't declared.
    private int index;

    // We need to stop player movement so we need to be able to target them (for now this is how I figured it out)
    [SerializeField] private GameObject player;

    private void Update()
    {
        // Checks to see if the printed text is equal to what we wrote in our dialogue box.
        if (dialogueText.text == dialogue[index])
        {
            // If the two texts are a match, we enable to continue button so we can move on to the next message.
            continueButton.SetActive(true);
        }

        // Not sure why this is needed, but I think it was because without it the player looks like they're falling.
        // This is because the player starts a little above the ground.
        // When I figure out/change their starting position, this may not be needed anymore.
        player.GetComponent<PlayerMovement>().UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On a collision, we need to make sure it is a player that triggered this event.
        // We don't want something else to trigger these messages.
        if (collision.CompareTag("Player"))
        {
            // We freeze the timer so players can read without worry.
            TimerSet.timerOn = false;
            
            // I manually change the player's velocity and stuff to make sure they don't slide away when reading.
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            player.GetComponent<PlayerMovement>().moveX = 0f;
            PlayerMovement.canMove = false;

            // Not sure why it is handled like this as the continue continue button only needs one listener.
            // In theory, I feel there should be no need to remove all listeners.
            // It may be for 'best practice' in case if the button has multiple uses or whatever.
            continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
            continueButton.GetComponent<Button>().onClick.AddListener(NextLine);

            // We want to make sure the dialogue UI is visible to the player but not the continue button right away.
            dialoguePanel.SetActive(true);
            continueButton.SetActive(false);

            // A coroutine begins running the 'typing' method.
            StartCoroutine(Typing());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // After hearing the tutorial once, we want to destroy it so it doesn't get read again.
        // Will probably change later and keep a boolean instead to turn it off so when a player dies, they don't have to read it again.
        Destroy(gameObject);
    }

    IEnumerator Typing()
    {
        // Kind of self explanatory.
        // For each letter in our dialogue array, we will print out that character one at a time.
        // The speed, of which, will be based on our variable 'wordSpeed'.
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Used to print out the next 'line' of text.
    public void NextLine()
    {
        // This continue button is set to false again here in case if an error happens.
        continueButton.SetActive(false);

        // If we have not yet reached the end of the available dialogue, we increment the index count to reach the next set of text.
        // Then, of course, we start coroutine of typing it out again.
        if (index < dialogue.Length -1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            // If there is no more text in our array, we want to reset our text boxes for the next event (if applicable).
            // We also turn on the timer again and give our player full movement once more.
            resetText();
            TimerSet.timerOn = true;
            PlayerMovement.canMove = true;
            player.GetComponent<PlayerMovement>().canFall = true;
        }
    }

    private void resetText()
    {
        // Essentially deletes any text in our variable and returns the index back to 0 for the next text (if any).
        dialogueText.text = "";
        index = 0;

        // Then we remove the UI box.
        dialoguePanel.SetActive(false);
    }
}
