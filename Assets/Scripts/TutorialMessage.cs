using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TutorialMessage : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogue;
    private int index;

    [SerializeField] private float wordSpeed = 0.05f;

    [SerializeField] private GameObject continueButton;

    // We need to stop player movement so we need to be able to target them (for now this is how I figured it out)
    [SerializeField] private GameObject player;

    private void Update()
    {
        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
        player.GetComponent<PlayerMovement>().UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TimerSet.timerOn = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        player.GetComponent<PlayerMovement>().moveX = 0f;
        player.GetComponent<PlayerMovement>().canMove = false;
        continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
        continueButton.GetComponent<Button>().onClick.AddListener(NextLine);
        dialoguePanel.SetActive(true);
        continueButton.SetActive(false);
        StartCoroutine(Typing());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < dialogue.Length -1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            resetText();
            TimerSet.timerOn = true;
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponent<PlayerMovement>().canFall = true;
        }
    }

    private void resetText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
}
