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
    [SerializeField] private float boxsizemodifierx = .75f;
    private float lastjumptime;
    private float jumpcooldown = .2f;
    [SerializeField] private Transform boxcaststart;

    private enum MovementState { idle, running, jumping, falling }


    // Start is called before the first frame update
    private void Start()
    {
        gameObject.tag = "Player";
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

        if(Time.time > lastjumptime+jumpcooldown&& IsGrounded())
            {
            jumpcounter = 0;
            }
        
        if(Input.GetButtonDown("Jump") && jumpcounter<(candoublejump ? 2:1))
            {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpcounter++;
            lastjumptime = Time.time;
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
        return Physics2D.OverlapBox(boxcaststart.position, new Vector2(coll.size.x * boxsizemodifierx, .1f), 0f, jumpableGround);
    }

   
}
