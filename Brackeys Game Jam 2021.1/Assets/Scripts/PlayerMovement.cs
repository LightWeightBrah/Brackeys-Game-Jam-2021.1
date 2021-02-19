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

    CharacterSwitch charSwitch;
    bool canJump;

    

    void Start()
    {
        charSwitch = GetComponent<CharacterSwitch>();
        rb = GetComponent<Rigidbody2D>();
     
    }

    void Update()
    {
        if (isPaused) return;



        if (Input.GetKeyDown(KeyCode.W) && jumpCounter > 0)
        {
            canJump = true;

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
            charSwitch.ChangeAnimationState(charSwitch.jumpAnim);
            rb.velocity = Vector2.up * jumpForce;
            jumpCounter--;
        }

        canJump = false;
    }

    public enum WalkMode { Reset, Idle, Run, Jump, Shoot };
    public WalkMode walkMode = WalkMode.Reset;

    private void FixedUpdate()
    {
        if (isPaused) return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {
            jumpCounter = extraJumps;
            if (walkMode == WalkMode.Reset)
            {
                charSwitch.ChangeAnimationState(charSwitch.idleAnim);
                walkMode = WalkMode.Idle;
            }
            else if (moveInput == 0 && walkMode != WalkMode.Idle && walkMode != WalkMode.Shoot)
            {
                charSwitch.ChangeAnimationState(charSwitch.idleAnim);
                walkMode = WalkMode.Idle;
            }
            else if (moveInput != 0 && walkMode != WalkMode.Run && walkMode != WalkMode.Shoot)
            {
                charSwitch.ChangeAnimationState(charSwitch.runAnim);
                walkMode = WalkMode.Run;
            }
            if (moveInput == 0) charSwitch.animator.SetBool("running", false);
        }
        Jump();
        if (walkMode != WalkMode.Shoot)
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
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
