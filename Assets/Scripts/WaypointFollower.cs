using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // Variable declarations.
    // IDE array variable in case if platforms have complex movements.
    [SerializeField] private GameObject[] waypoints;

    // To check our current waypoint.
    private int currentWaypointIndex = 0;

    // Platform speed
    [SerializeField] private float speed = 4f;

    private void Update()
    {
        // We check to see if the object has reached the first waypoint.
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.01f)
        {
            // If it has, we change to the next waypoint by incrementing our index.
            currentWaypointIndex++;

            // Below checks to see if the current index exceeds the number of way points avalable.
            if (currentWaypointIndex >= waypoints.Length)
            {
                // If we have reached an index beyond the available waypoints, we reset it to the beginning.
                // This is basic and should be changed as multiple way points isn't going to work.
                // For now, leaving as is until I have to change it.
                // For reference, the index should be reverese incrementally so it doesn't teleport back to the starting point.
                currentWaypointIndex = 0;
            }
        }

        // If the object (platform in this case) hasn't reached the end yet, we move it towards the way point position.
        // This movement is based on speed per frame.
        transform.position = Vector2.MoveTowards(
            transform.position, 
            waypoints[currentWaypointIndex].transform.position, 
            Time.deltaTime * speed);
    }
}
