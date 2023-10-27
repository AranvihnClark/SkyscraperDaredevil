using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWings : MonoBehaviour
{
    // Variable declarations.
    [SerializeField] private GameObject fallingPlatform;

    private Animator wings;

    private void Start()
    {
        // Initializing our variable.
        wings = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (fallingPlatform.GetComponent<FallingPlatform>().isTriggered)
        {
            // If the variable from the script above is true, we want to change the temp wings' animation to 'disappear'.
            wings.SetBool("OnPlatform", true);

            // An Invoke is used as we want to wait until the animation is completed before it deletes the wings completely.
            Invoke("DestroySelf", 0.5f);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
