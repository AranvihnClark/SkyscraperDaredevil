using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    
    private SpriteRenderer sprite;
    private Rigidbody2D rg;
    private float alphaTime = 0.8f;
    private bool isCollided = false;

    public bool isTriggered = false;

    private void Start()
     {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rg = gameObject.GetComponent<Rigidbody2D>();
     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    private void Update()
    {
        if (isTriggered)
        {
            if (alphaTime > 0f)
            {
                alphaTime -= Time.deltaTime;
            }
            else 
            {
                rg.bodyType = RigidbodyType2D.Dynamic;
                rg.gravityScale = 5f;
                rg.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if (isTriggered && isCollided)
        {
            sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - (2.5f * Time.deltaTime));
            if (sprite.color.a <= 0f)
            {
                PlatformVanish();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {}
        else if (collision.gameObject.CompareTag("FallThrough")) {}
        else
        {
            isCollided = true;
        }
    }

    private void PlatformVanish()
    {
        Destroy(gameObject);
    }

}
