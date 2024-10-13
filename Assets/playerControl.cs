using System.Data;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 15f;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    public GameObject interactUI;
    public GameObject box1;
    public GameObject endscreen;

    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;

    [Header("Ground Detection")]
    public Transform groundCheck;

    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator component

    // Ladder variables
    private bool isOnLadder = false;
    private float climbSpeed = 5f;
    private float climbInput;
    private bool isJumpingOffLadder = false;

    // Coyote time variables
    private float coyoteTime = 0.2f; // How long the player can jump after leaving the ground
    private float coyoteTimeCounter; // Timer for coyote time

    public leverScript LeverScript;
    public leverScript LeverScript2;
    public leverScript LeverScript3;
    public leverScript LeverScript4;
    public leverScript LeverScript5;
    public leverScript LeverScript6;

    public AudioSource jumpSFX;
    public AudioSource deathSFX;
    public AudioSource winSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        endscreen.SetActive(false);
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset the coyote time counter if the player is grounded
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset the timer when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrease the timer if not grounded
        }

        // Handle movement input
        moveInput = Input.GetAxis("Horizontal");

        // Update the Animator's "speed" parameter, but only if the player is not on a ladder
        animator.SetFloat("speed", isOnLadder ? 0 : Mathf.Abs(moveInput));

        // Jumping logic with coyote time
        if ((coyoteTimeCounter > 0 || isOnLadder) && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumpingOffLadder = isOnLadder; // Player jumps off ladder
            coyoteTimeCounter = 0; // Reset the coyote time to prevent multiple jumps
            jumpSFX.Play();
        }

        // Flip the player sprite when changing direction
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Ladder Climbing Input
        if (isOnLadder && !isJumpingOffLadder)
        {
            climbInput = Input.GetAxis("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(1, -2, 0);
            LeverScript.buttonFlipped = false;
            LeverScript2.buttonFlipped = false;
            LeverScript3.buttonFlipped = true;
            box1.transform.position = new Vector3(23, 21, 0);
            LeverScript4.buttonFlipped = false;
            LeverScript5.buttonFlipped = true;
            LeverScript6.buttonFlipped = false;
        }
    }

    void FixedUpdate()
    {
        if (isOnLadder && !isJumpingOffLadder)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, climbInput * climbSpeed);
            rb.gravityScale = 0f; // Disable gravity while climbing
        }
        else
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            rb.gravityScale = 2f; // Restore gravity for normal movement or jumping
        }

        // Reset ladder jump state when grounded
        if (isGrounded)
        {
            isJumpingOffLadder = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the player sprite
        transform.localScale = theScale;
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ladder"))
        {
            isOnLadder = true;
        }
        if (other.CompareTag("cola"))
        {
            gameObject.transform.position = new Vector3(1, -2, 0);
            LeverScript.buttonFlipped = false;
            LeverScript2.buttonFlipped = false;
            LeverScript3.buttonFlipped = true;
            box1.transform.position = new Vector3(23, 21, 0);
            LeverScript4.buttonFlipped = false;
            LeverScript5.buttonFlipped = true;
            LeverScript6.buttonFlipped = false;
            deathSFX.Play();
        }
        if (other.CompareTag("win"))
        {
            endscreen.SetActive(true);
            winSFX.Play();
            Destroy(gameObject, 3);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ladder"))
        {
            isOnLadder = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("lever"))
        {
            interactUI.SetActive(true);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("lever"))
        {
            interactUI.SetActive(false);
        }
    }
}
