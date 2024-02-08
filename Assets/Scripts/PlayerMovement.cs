using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    Rigidbody2D rb;
    public float player_speed;
    [SerializeField]  bool isFacingRight;

    [Header("Jumping Variables")]
    [SerializeField] float jumpTime;
    [SerializeField] int jump_height;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    private bool isJumping;
    private bool doubleJump;
    private float jumpCounter = 0;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 vecGravity;

    [Header("Dashing Variables")]
    public bool canDash = true;
    [SerializeField] bool isDashing;
    public float dashingSpeed;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1.0f;
    private TrailRenderer dashTrail;
    [SerializeField] float dashGrav;
    private float originalGrav;
    private float waitTime;

    Vector2 vecMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTrail = GetComponent<TrailRenderer>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        canDash = true;
        isFacingRight = true;
        originalGrav = rb.gravityScale;
    }


    public void Jump(InputAction.CallbackContext value)
    {
        
        if (value.started)
        {
            if(gameObject.name == "Player2" && isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_height);
                isJumping = true;
                jumpCounter = 0;
            }
            if(gameObject.name == "Player1")
            {
                if (isGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump_height);
                    isJumping = true;
                    doubleJump = true;
                    jumpCounter = 0;
                }
                else if (doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump_height * 0.7f);
                    doubleJump = false;
                    jumpCounter = 0;
                }
            }
            
        }
        if (value.canceled)
        {
            isJumping = false;
            if(isGrounded()) jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }
    }

    public void Movement(InputAction.CallbackContext value)
    {
        //Debug.Log(value.ReadValue<Vector2>());
        vecMove = value.ReadValue<Vector2>();
        flip();
    }

    public void Dash(InputAction.CallbackContext value)
    {
       
        if (value.performed && canDash)
        {
            if(waitTime >= dashingCooldown)
            {
                waitTime = 0;
                Invoke("Dash", 0);
            }
        }


    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        if (isDashing)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(vecMove.x * player_speed, rb.velocity.y);
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }
    }

    void flip()
    {
        if (vecMove.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (vecMove.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    public void Dash()
    {
        canDash = false;
        isDashing = true;
        dashTrail.emitting = true;
        rb.gravityScale = dashGrav;

        if(vecMove.x == 0)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashingSpeed, 0);
            }
            if (!isFacingRight)
            {
                rb.velocity = new Vector2(-transform.localScale.x * dashingSpeed, 0);
            }
        }
        else
        {
            rb.velocity = new Vector2(vecMove.x * dashingSpeed, 0);
        }
        Invoke("StopDash", dashingTime);

    }

    public void StopDash()
    {
        canDash = true;
        isDashing = false;
        dashTrail.emitting = false;
        rb.gravityScale = originalGrav;
    }
}
