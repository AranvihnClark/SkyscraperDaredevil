using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    [SerializeField] private GameObject redSwtich;
    private BoxCollider2D bc2D;

    private Animator electricityOn;
    private void Start()
    {
        electricityOn = gameObject.GetComponent<Animator>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (redSwtich.GetComponent<RedSwitch>().isFlipped)
        {
            electricityOn.SetBool("Off", true);
            bc2D.enabled = false;
        }
        if (!redSwtich.GetComponent<RedSwitch>().isFlipped)
        {
            electricityOn.SetBool("Off", false);
            bc2D.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDeath>().Death();
        }
    }
}
