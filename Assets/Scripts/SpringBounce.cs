using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

public class SpringBounce : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D[] bc2D;
    private float modifier = 0.1f;

    private bool isBouncing = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bc2D = GetComponents<BoxCollider2D>();
    }
    
    private void Update()
    {
        if (isBouncing)
        {
            bc2D[1].size = new UnityEngine.Vector2(bc2D[1].size.x, bc2D[1].size.y - modifier);

            if (bc2D[1].size.y <= 0.01)
            {
                modifier = -modifier;
            }
            if (isBouncing && bc2D[1].size.y > 0.85f)
            {
                isBouncing = false;
                bc2D[1].size = new UnityEngine.Vector2(bc2D[1].size.x, 0.85f);
                modifier = -modifier;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("onTouch");
        isBouncing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isBouncing = false;
    }
}
