using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 7f;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool facingRight = true;

    public TeleportBehaviour TeleportPrefab;
    public Transform LaunchOffset;

    private TeleportDestinationFollower destinationFollower; // Reference to the TeleportDestinationFollower script

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        destinationFollower = FindObjectOfType<TeleportDestinationFollower>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        anim.SetBool("Walk", horizontalInput != 0);

        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            TeleportBehaviour teleportInstance = Instantiate(TeleportPrefab, LaunchOffset.position, LaunchOffset.transform.rotation);
            // Set the player's position to match the teleportDestination, even if it's destroyed
            if (teleportInstance.TeleportDestination != null)
            {
                transform.position = teleportInstance.TeleportDestination.transform.position;
            }
            destinationFollower.teleport2 = teleportInstance.transform;
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        anim.SetBool("Jump", true);
        grounded = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool("Jump", false);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
        LaunchOffset.transform.Rotate(0f, 180f, 0f);
    }
}
