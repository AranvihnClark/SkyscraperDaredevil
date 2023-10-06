using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{
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

    // Finish Line Audio Variable
    private AudioSource finishLineSE;
    private bool isFinished = false;

    private void Start()
    {
        finishLineSE = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (dialogueTotalMessage.text == dialogue[4])
        {
            continueButton.SetActive(true);
        }
        player.GetComponent<PlayerMovement>().UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !isFinished)
        {
            finishLineSE.Play();
            isFinished = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0);
            player.GetComponent<PlayerMovement>().moveX = 0f;
            player.GetComponent<PlayerMovement>().canMove = false;
            dialoguePanel.SetActive(true);
            continueButton.SetActive(false);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(Typing());
        }
    }

    IEnumerator Typing()
    {
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
    public void NextLine()
    {
        continueButton.SetActive(false);
        resetText();
    }

    private void resetText()
    {
        dialogueLevelMessage.text = "";
        dialogueListMessage.text = "";
        dialogueTimeMessage.text = "";
        dialogueItemsMessage.text = "";
        dialogueTotalMessage.text = "";
        dialoguePanel.SetActive(false);
        Invoke("CompleteLevel", 0.1f);
    }

    private void CompleteLevel()
    {
        player.GetComponent<PlayerMovement>().canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
