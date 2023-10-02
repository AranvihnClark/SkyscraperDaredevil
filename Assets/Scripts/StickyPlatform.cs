using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // gameObject in this case is always in reference to the object in Unity's Hierarchy list.
        if (collision.gameObject.name == "Player")
        {
            // 'transform' refers to the StickyPlatform's script's game object's transform info.
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // gameObject in this case is always in reference to the object in Unity's Hierarchy list.
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
