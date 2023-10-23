using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDestinationFollower : MonoBehaviour
{
    public Transform teleport2; // Reference to the object to follow (teleport2)

    void Update()
    {
        if (teleport2 != null)
        {
            // Check if teleport2 is not null before accessing its position
            transform.position = teleport2.position;
        }
    }
}
