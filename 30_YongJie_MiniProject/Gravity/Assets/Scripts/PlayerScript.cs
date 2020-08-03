using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public int gravityMeter;

    Animator playerAnimator;
    Rigidbody2D rb2d;
    bool doubleJumping = false;
    bool onGravity = false;
    float movement;
    int jumpState;

    void Start()
    {      
        playerAnimator = GetComponent<Animator>();
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        int jumpState = playerAnimator.GetInteger("JumpState");
        if (rb2d.velocity.y < 0 && jumpState > 0)
        {
            playerAnimator.SetInteger("JumpState", 3);
        }

        if(Input.GetKeyDown(KeyCode.W) && !onGravity)
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");

        // Update position
        if(!onGravity)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(movement));
            if (movement > 0)
            {
                rb2d.velocity = new Vector2(movement * moveSpeed, rb2d.velocity.y);
                facingDirection();
            }
            else if (movement < 0)
            {
                rb2d.velocity = new Vector2(movement * moveSpeed, rb2d.velocity.y);
                facingDirection();
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
        else
        {

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {          
            gravitySwap();
        }
        
        //---------------------------------------------------------//
    }

    void facingDirection()
    {
        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void jump()
    {
        if(doubleJumping)
        {
            return;
        }

        Vector2 velocity = rb2d.velocity;
        velocity.y = jumpForce;
        rb2d.velocity = velocity;

        jumpState = playerAnimator.GetInteger("JumpState");
        if (jumpState == 0 || jumpState == 1)
        {
            playerAnimator.SetInteger("JumpState", jumpState + 1);
            if (jumpState == 1)
            {
                doubleJumping = true;
            }
        }
        else if (jumpState == 3)
        {
            playerAnimator.SetInteger("JumpState", 2);
            doubleJumping = true;
        }

    }

    void gravitySwap()
    {
        if(!onGravity)
        {
            onGravity = true;
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            onGravity = false;
            rb2d.gravityScale = 1;
            return;
        }
                         
        StartCoroutine(floatUp());

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        playerAnimator.SetInteger("JumpState", 0);
        doubleJumping = false;
    }

    IEnumerator floatUp()
    {
        jumpState = playerAnimator.GetInteger("JumpState");
        playerAnimator.SetFloat("Speed", 0);

        if (jumpState == 0)
        {
            Vector2 velocity = rb2d.velocity;
            velocity.y = jumpForce;
            rb2d.velocity = velocity;

        }
        yield return new WaitForSeconds(0.15f);
        rb2d.velocity = Vector2.zero;

    }

    /*
     if(onGravity == false)
        {      
            rb2d.gravityScale = 5;         

            movement = Input.GetAxis("Horizontal");
            if (movement > 0)
            {
                rb2d.velocity = new Vector2(movement * moveSpeed, rb2d.velocity.y);
            }
            else if (movement < 0)
            {
                rb2d.velocity = new Vector2(movement * moveSpeed, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }

            // for player faceing direction //
            if (movement < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (movement > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            //------------------------------//


            if (isGrounded == true)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    isGrounded = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetTrigger("gravityStandby");
                playerAnimator.SetBool("onGravity", true);
                onGravity = true;            
                print(onGravity);

            }
        }
        else if(onGravity == true)
        {           
            rb2d.gravityScale = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                onGravity = false;
                playerAnimator.SetBool("onGravity", false);
                print(onGravity);
                
            }
        }
     */

}
