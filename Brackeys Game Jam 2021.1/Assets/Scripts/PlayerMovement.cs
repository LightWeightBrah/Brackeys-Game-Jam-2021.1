using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    float moveInput;
    bool facingRight = true;

    public int extraJumps;
    int jumpCounter;

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    bool isGrounded;

    public bool isPaused;

    public Rigidbody2D rb;

    private FMOD.Studio.EventInstance jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/player_jump");
    }

    void Update()
    {
        if (isPaused) return;

        if(isGrounded)
        {
            jumpCounter = extraJumps;
        }

        if(Input.GetKeyDown(KeyCode.W) && jumpCounter > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpCounter--;
            jumpSound.start();
            //jumpSound.release();
        }

        moveInput = Input.GetAxis("Horizontal");

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;

        if(facingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, 0f);
        }
    }
}
