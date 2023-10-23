using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehaviour : MonoBehaviour
{
    
    public float Speed = 4.5f;

    public GameObject TeleportDestination;

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        // Set TeleportDestination to the current location
         TeleportDestination = transform.gameObject;
        // Destroy the gameObject
        Destroy(gameObject);
        //Destroy(gameObject); 
=======
        Destroy(gameObject); 
>>>>>>> d4b4981e8f902af6190563156090e7f1efdb6324
    }


}
