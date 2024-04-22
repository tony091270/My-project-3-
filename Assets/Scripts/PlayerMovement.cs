using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    public bool candoublejump;
    private int jumpcounter;

    private float dirX = 0f;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running, jumping, falling }


    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Hello, World!");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    

        
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpcounter++;
            Debug.Log("jump1test");
        }
        Debug.Log(jumpcounter);
        Debug.Log(IsGrounded());

        if (Input.GetButtonDown("Jump") && jumpcounter<2)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
            jumpcounter++;
        }

        


            UpdateAnimationState();
    }


    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }


        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        var boxcastsize = coll.bounds.size;
        boxcastsize.y *= 0.9f;
        boxcastsize.x *= 0.9f;
        var result = Physics2D.BoxCast(coll.bounds.center, boxcastsize, 0f, Vector2.down, .1f, jumpableGround);
        if(result.collider !=null)
        {
            jumpcounter = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

   
}
