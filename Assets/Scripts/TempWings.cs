using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWings : MonoBehaviour
{
    [SerializeField] private GameObject fallingPlatform;

    private Animator wings;

    private void Start()
    {
        wings = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (fallingPlatform.GetComponent<FallingPlatform>().isTriggered)
        {
            wings.SetBool("OnPlatform", true);
            Invoke("DestroySelf", 0.5f);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
