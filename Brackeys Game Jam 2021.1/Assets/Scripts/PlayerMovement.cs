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

    PlayerShooting playerShooting;

    CharacterSwitch charSwitch;
    bool canJump;

    private FMOD.Studio.EventInstance jumpSound;

    void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();
        charSwitch = GetComponent<CharacterSwitch>();
        rb = GetComponent<Rigidbody2D>();
        jumpSound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/player_jump");
    }

    void Update()
    {
        if (isPaused) return;



        if (Input.GetKeyDown(KeyCode.W) && jumpCounter > 0)
        {
            canJump = true;

            jumpSound.start();
            jumpSound.release();

        }

        moveInput = Input.GetAxis("Horizontal");




        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(canJump);
            Debug.Log("Ground check " + isGrounded);
            Debug.Log("Move input " + moveInput);
            Debug.Log("CharSwitch.currentAnimationState  " + charSwitch.currentAnimationState);
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            walkMode = WalkMode.Jump;
            jumpSound.start();
            charSwitch.ChangeAnimationState(charSwitch.jumpAnim);
            rb.velocity = Vector2.up * jumpForce;
            jumpCounter--;
        }

        canJump = false;
    }

    public enum WalkMode { Idle, Run, Jump };
    WalkMode walkMode = WalkMode.Idle;

    private void FixedUpdate()
    {
        if (isPaused) return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(playerShooting.isShooting)
        {
            Vector2 vel = new Vector2(0f, rb.velocity.y);
            rb.velocity = vel;
            return;
        }

        if (isGrounded)
        {
            jumpCounter = extraJumps;
            if (moveInput == 0 && walkMode != WalkMode.Idle)
            {
                charSwitch.ChangeAnimationState(charSwitch.idleAnim);
                walkMode = WalkMode.Idle;
            }
            else if (moveInput != 0 && walkMode != WalkMode.Run)
            {
                charSwitch.ChangeAnimationState(charSwitch.runAnim);
                walkMode = WalkMode.Run;
            }
        }
        Jump();
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;

        if (facingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, 0f);
        }
    }
}
