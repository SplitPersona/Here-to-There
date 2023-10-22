using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehaviour : MonoBehaviour
{
    public float Speed = 4.5f;

    private void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroys(GameObject); 
    }


}