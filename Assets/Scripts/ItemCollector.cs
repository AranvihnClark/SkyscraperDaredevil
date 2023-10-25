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
    public static int skyTokens = 0;
    public static bool keyCollected = false;

    // Using Text Mesh Pro - so datatype is known as below.
    [SerializeField] private TextMeshProUGUI skyTokenCount;

    // Item Audio Variables
    [SerializeField] private AudioSource itemSE;

    // On collision, the below triggers.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SkyToken"))
        {
            itemSE.Play();
            skyTokens++;
            skyTokenCount.text = "Sky Tokens: " + skyTokens;

            // Destroys the object once 'collected' by the player.
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("RedKey"))
        {
            itemSE.Play();
            keyCollected = true;
            Destroy(collision.gameObject);
        }
    }
}
