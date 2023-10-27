using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemCollector : MonoBehaviour
{
    // Using Text Mesh Pro - so datatype is known as below.
    [SerializeField] private TextMeshProUGUI skyTokenCount;

    // Item Audio Variables
    [SerializeField] private AudioSource itemSE;

    // General variable declarations.
    // Public is used for other scripts to access.
    public static int skyTokens = 0;
    public static bool keyCollected = false;


    // On collision, the below triggers.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Basically checks to see if the item collected is a token and plays a sound and increments our token count.
        if (collision.gameObject.CompareTag("SkyToken"))
        {
            itemSE.Play();
            skyTokens++;
            skyTokenCount.text = "Sky Tokens: " + skyTokens;

            // Destroys the object once 'collected' by the player.
            Destroy(collision.gameObject);
        }

        // If the item collected is a key...
        if (collision.gameObject.CompareTag("RedKey"))
        {
            itemSE.Play();
            keyCollected = true;
            Destroy(collision.gameObject);
        }
    }
}
