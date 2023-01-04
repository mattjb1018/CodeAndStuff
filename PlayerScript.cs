using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float moveSpeed;
    private bool facingRight;
    private Animator myAnimator;

    [SerializeField]
    private Transform[] groundPoints; //creates an array of "points"

    [SerializeField]
    private float groundRadius; //creates the size of the colliders

    [SerializeField]
    private LayerMask whatIsGround; //defines what is ground

    private bool isGrounded; //can be set to true or false

    private bool jump; //can be set to true or false when we press spacebar

    [SerializeField]
    private float jumpForce; //determines the magnitude of the jump

    public bool isAlive;

    public GameObject reset;

    private Slider healthBar;
    public float health = 3f;
    private float healthBurn = 1f;

   private bool canDash = true; // indicates if the player can dash or not
   private bool isDashing; //indicates if the player is already dashing
   private float dashingPower = 24f; //determines the power of the dash
   private float dashingTime = 0.2f; //determines how long the dash will last
   private float dashingCooldown = 1f; //determines when the player can dash again
   [SerializeField] private TrailRenderer tr; //in reference of the trail we added to the player
   [SerializeField] private Rigidbody2D rb; //for convenience

     //&& AND , || OR, == SAME AS, != NOT THE SAME SA

     // Start is called before the first frame update
     void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  //a variable to control the Player's body
        facingRight = true;
        myAnimator = GetComponent<Animator>();
        isAlive = true;
        reset.gameObject.SetActive(false);
        healthBar = GameObject.Find("health slider").GetComponent<Slider>();
        healthBar.minValue = 0f;
        healthBar.maxValue = health;
        healthBar.value = healthBar.maxValue;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");     //a variable that stores the value of out horizontal movement
        //Debug.Log(horizontal);
         //function that controls player on the x-axis
        if (isAlive)
        {
            Flip(horizontal);
            HandleInput();
            PlayerMovement(horizontal);
        } else {
            return;
                }
        isGrounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.RightShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

    }

    //Funtion Definitions
    private void PlayerMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            jump = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        myRigidbody.velocity = new Vector2(horizontal * moveSpeed, myRigidbody.velocity.y); //adds velocity to the player's body on the a-xis
        myAnimator.SetFloat("speed",Mathf.Abs(horizontal));
       // myAnimator.SetBool("jumping", !isGrounded); 
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            myAnimator.SetBool("jumping", true);
            //Debug.Log("I'm jumping Pog"); ;
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal<0 && facingRight || horizontal>0 && !facingRight)
        {
            facingRight = !facingRight; //resets the bool to the oppisite
            Vector2 theScale = transform.localScale; //creating vector 2 and storing the local scale values
            theScale.x *= -1;   //
            transform.localScale = theScale;
        }
    }

    public void ImDead()
    {
        isAlive = false;
        myAnimator.SetBool("dead", true);
        reset.gameObject.SetActive(true);
        healthBar.value = 0f;
        health = 0f;
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "ground")
        {
            myAnimator.SetBool("jumping", false);
        }
        if (target.gameObject.tag == "deadly")
        {
            ImDead(); 
        }
        if (target.gameObject.tag == "damage")
        {
            UpdateHealth();
        }
    }

    void UpdateHealth()
    {
        if (health > 0)
        {
            health -= healthBurn; //health equals health minus health burn
            healthBar.value = health;
        }
        if (health <= 0)
        {
            ImDead();
        }
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {                       
                        return true;
                    }
                }

            }
        }
        return false;
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; //this will allow the player to not be affected by gravity when dashing
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f); //controling the velocity of the dash + where you are facing
        tr.emitting = true; //makes the trail only appear when dashing
        yield return new WaitForSeconds(dashingTime); //forces the player to not be able to dash forever
        tr.emitting = false;
        rb.gravityScale = originalGravity; //makes the player go back to his gravity to when before he was dashing
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true; 
    }



}



