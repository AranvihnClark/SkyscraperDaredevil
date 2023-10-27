using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Note, this was created as part of a 'tutorial' to learn other ways to affect objects.
    // This could have also been easily done with an animation for the object.
    // Though, for me, this would probably be faster than drawing a picture 6-8 times to rotate it.
    // (Though it's probably just as easy if you get used to shortcuts and stuff.)

    // Variable declaration.
    [SerializeField] float speed = -1f;

    private void Update()
    {
        // Rotates the object this script is assigned to based on speed (variable above) per frames (time)
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
