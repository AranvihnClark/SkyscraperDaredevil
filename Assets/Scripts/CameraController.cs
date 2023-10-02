using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variable to hold our player's transform info.
    // In this case, the SerializeField allows us to 'assign' an object's transform to this script.
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
