using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int strawberries = 0;

    // Using Text Mesh Pro - so datatype is known as below.
    [SerializeField] private TextMeshProUGUI strawberryCount;

    // Item Audio Variables
    [SerializeField] private AudioSource itemSE;

    // On collision, the below triggers.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            itemSE.Play();
            strawberries++;
            strawberryCount.text = "Strawberries: " + strawberries;

            // Destroys the object once 'collected' by the player.
            Destroy(collision.gameObject);
        }
    }
}
