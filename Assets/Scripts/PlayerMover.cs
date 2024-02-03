using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    Rigidbody2D rb;
    public float player_speed;

    [SerializeField] float jumpTime;
    [SerializeField] int jump_height;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;

    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 vecGravity;

    bool isJumping;
    float jumpCounter;


    Vector2 vecMove;

    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }


    public void Jump(InputAction.CallbackContext value)
    {
        Debug.Log(value.phase);
        if (value.started && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump_height);
            isJumping = true;
            jumpCounter = 0;
        }
        if(value.canceled)
        {
            isJumping = false;
            jumpCounter = 0;
            
            if(rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }
    }
    
    public void Movement(InputAction.CallbackContext value) 
    {
        Debug.Log(value.ReadValue<Vector2>());
        vecMove = value.ReadValue<Vector2>();
        flip();
    }

    private void FixedUpdate()
    {
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
}
