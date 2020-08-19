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
    bool facingLeft;
    float movement;
    int jumpState;

    AudioSource Audio;
    Vector2 mousePos;
    Vector2 playerPos;

    public int maxhealth = 100;
    public int currenthealth;
    public HealthBarScript healthscript;

    public int maxother = 100;
    public int currentother;
    public int consumerate;
    public OtherBarScript otherscript;
    void Start()
    {
        currenthealth = maxhealth;
        healthscript.SetMaxHealth(maxhealth);
        currentother = maxother;
        otherscript.SetMaxother(maxother);
        Audio = GetComponent<AudioSource>();
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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            UseOther(10);
        }
        currentother = Mathf.FloorToInt(otherscript.bar.value);
    }
    void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");
       
        // Update position
        if (!onGravity)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(movement));
            if (movement > 0)
            {
                rb2d.velocity = new Vector2(movement * moveSpeed, rb2d.velocity.y);
                facingDirection(facingLeft);
            }
            else if (movement < 0)
            {
                rb2d.velocity = new Vector2(movement * moveSpeed, rb2d.velocity.y);
                facingDirection(!facingLeft);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }       
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                playerPos = transform.position;
                print(mousePos);              

                if(mousePos.x > playerPos.x)
                {
                    facingDirection(facingLeft);
                }
                else if(mousePos.x < playerPos.x)
                {
                    facingDirection(!facingLeft);
                }

                playerAnimator.SetTrigger("Attack");
                transform.position = mousePos;
                Audio.Play();
            }
            otherscript.bar.value -= consumerate * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {          
            gravitySwap();
        }
        
        //---------------------------------------------------------//
    }

    void facingDirection(bool facing_Dir)
    {
        if(facing_Dir)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
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
            otherscript.regen = 0;
        }
        else
        {
            onGravity = false;
            rb2d.gravityScale = 1;
            otherscript.regen = otherscript.originalregen;
            return;
        }
        StartCoroutine(floatUp());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        playerAnimator.SetInteger("JumpState", 0);
        doubleJumping = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            print("Hit");
        }
    }

    void TakeDamage(int damage)
    {
        currenthealth -= damage;
        healthscript.SetHealth(currenthealth);
    }

    void UseOther(int usage)
    {
        currentother -= usage;
        otherscript.SetOther(currentother);
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
