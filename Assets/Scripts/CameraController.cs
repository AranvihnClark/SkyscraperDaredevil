using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variable to hold our player's transform info.
    // In this case, the SerializeField allows us to 'assign' an object's transform to this script.
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        // Change's our camera's position, per frame, based on where our player's position is.
        // I do wonder if it's necessary for the 'z' position to be there but it would make sense in a '2/3D' style game.
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
