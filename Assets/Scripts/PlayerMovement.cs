using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 7f;

    public bool canDoubleJump = false;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool facingRight = true;

    public TeleportBehaviour TeleportPrefab;
    public Transform LaunchOffset;

    private TeleportDestinationFollower destinationFollower; // Reference to the TeleportDestinationFollower script
    private GameObject TeleportObject; // Reference to the Fire object

    private TeleportBehaviour currentTeleport; // Track the current teleport instance

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        destinationFollower = FindObjectOfType<TeleportDestinationFollower>();
    }

    private void Start ()

    {
        // Find the Fire object in the scene (you may need to replace "Fire" with the actual tag or name of the object)
        TeleportObject = GameObject.FindWithTag("Teleporter");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        anim.SetBool("Walk", horizontalInput != 0);

        /*if(horizontalInput != 0 && grounded)
        {
            AudioManager.instance.PlayFootstepSound();
        }*/
        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
        {
            Flip();
        }

        if(Input.GetKey(KeyCode.Space)&& grounded)
        {
            canDoubleJump = true; 
            Jump();
             
        }
        else if(Input.GetKeyDown(KeyCode.Space)&&canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentTeleport == null)
            {
                TeleportBehaviour teleportInstance = Instantiate(TeleportPrefab, LaunchOffset.position, LaunchOffset.transform.rotation);
                currentTeleport = teleportInstance;
                destinationFollower.teleport2 = teleportInstance.transform;
            }
        }

        if (currentTeleport != null && !currentTeleport.gameObject.activeSelf)
        {
            TeleportPlayer();
            currentTeleport = null;
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        anim.SetBool("Jump", true);
        grounded = false;
        AudioManager.instance.PlayJumpSound();
    }

    private void TeleportPlayer()
    {
        if (currentTeleport != null)
        {
            transform.position = currentTeleport.TeleportDestination.transform.position;
        }
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
