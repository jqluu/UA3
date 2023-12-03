using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public LogicScript logic;
    public bool PlayerIsAlive = true;

    [SerializeField]
    float walkSpeed = 5f;
    [SerializeField]
    float jumpForce = 2f;
    [SerializeField]
    int maxJumps = 2; 
    int jumpCount = 0; 
    [SerializeField]
    float maxYGroundedCheck = 2.0f; 

    Vector2 moveInput;


    [SerializeField]
    public static bool isJumping = false;
   

    bool IsGrounded()
    {
        return transform.position.y < maxYGroundedCheck;
    }

    private bool _isMoving = false;
    public bool IsMoving { get 
        {
            return _isMoving;
        } 
    private set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    
    Rigidbody2D rb;
    Animator animator;

    public bool _isFacingRight = true;
    public bool IsFacingRight { get 
        {
            return _isFacingRight;
        } 
    private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public Vector2 GetFacingDirection()
    {
        if (IsFacingRight) // Facing left
            {
                return Vector2.right;
            }
            else // Facing right
            {
                return Vector2.left;
            }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        bool grounded = IsGrounded();

        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);

        if (grounded)
        {
            jumpCount = 0;
        }
        else
        {
            animator.SetBool("IsJumping", isJumping);
        }

        if (isJumping && jumpCount< maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
            

            jumpCount++;
        }       

        
    }




    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

       public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump called!");


        if (context.started)
        {
            isJumping = true;
            if(isJumping)
                Debug.Log("isJumping set to true!"); 
        }
    }



   private void SetFacingDirection(Vector2 moveInput)
    {   
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face the left
            IsFacingRight = false;
        }
        else if (moveInput.x == 0)
        {
            // Keep the current direction when not moving horizontally
            // This prevents unnecessary flipping while idle
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
    {
        // The collision is with an object tagged as "Enemy"
        logic.gameOver();
        PlayerIsAlive = false;
        Destroy(gameObject);
        logic.playerScore = 0;
    }
    }

    
}
