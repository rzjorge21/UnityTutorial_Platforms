using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Player _player;
    private Rigidbody2D rb;

    public Animator anim;

    public float movementSpeed = 5f;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    private bool grounded;
    private bool canDoubleJump = false;
    public LayerMask whatIsGround;
    private bool stoppedJumping;

    public Transform groundCheck;
    public float groundCheckRadius;

    public GameManager _gameManager;

    [Header("Hit")]
    public float hitRadius;
    public LayerMask whatIsGroundHit;

    private bool top, bottom, left, right, die;


    private void Awake()
    {
        //_player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");


        Flip(x);

        rb.velocity = new Vector2(x * movementSpeed, rb.velocity.y);

        Jump();

        //Animation
        anim.SetInteger("velX", (int)rb.velocity.x);
        anim.SetInteger("velY", (int)rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        
        //Hit
        left = Physics2D.OverlapCircle(transform.position + new Vector3(-0.33f,-0.33f,0f), hitRadius, whatIsGroundHit);
        right = Physics2D.OverlapCircle(transform.position + new Vector3(0.33f,-0.33f,0f), hitRadius, whatIsGroundHit);
        top = Physics2D.OverlapCircle(transform.position + new Vector3(0f,0.33f,0f), hitRadius, whatIsGroundHit);
        bottom = Physics2D.OverlapCircle(transform.position + new Vector3(0f,-0.67f,0f), hitRadius, whatIsGroundHit);

        if(!die){
            if(top && bottom){
                Die();
            }
            if(left && right){
                Die();
            }
        }   
    }

    void Jump()
    {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (grounded && rb.velocity.y <= 0f)
        {
            jumpTimeCounter = jumpTime;
            anim.SetBool("canDoubleJump", false);
        }
        //I placed this code in FixedUpdate because we are using phyics to move.

        //if you press down the mouse button...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //and you are on the ground...
            if (grounded)
            {
                //jump!
                anim.SetBool("canDoubleJump", false);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
                canDoubleJump = true;
                
            }
            else
            {
                
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    anim.SetBool("canDoubleJump", true);
                    //rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce/1.25f);
                    
                }
            }
        }

        if ((Input.GetKey(KeyCode.Space)) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }

    void Flip(float dir)
    {
        if (dir < 0) GetComponent<SpriteRenderer>().flipX = true;
        else if (dir > 0) GetComponent<SpriteRenderer>().flipX = false;
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(transform.position + new Vector3(-0.33f,-0.33f, 0f), 0.1f);
    //     Gizmos.DrawSphere(transform.position + new Vector3(0.33f,-0.33f, 0f), 0.1f);
    //     Gizmos.DrawSphere(transform.position + new Vector3(0f,0.33f, 0f), 0.1f);
    //     Gizmos.DrawSphere(transform.position + new Vector3(0f,-0.67f, 0f), 0.1f);
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            Die();
        }
        if(collision.gameObject.layer == 9){
            gameObject.transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.layer == 9){
            gameObject.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            collision.gameObject.GetComponent<Fruit>().DestroyFruit();
            _gameManager.CheckWin();
        }
        
    }

    void Die()
    {
        die = true;
        anim.SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        _gameManager.GameOver();
    }
}
