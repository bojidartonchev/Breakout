using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public Animation Jump;
    public float moveSpeed;
    Animator anim;
    public float jumpForce;
    //float VelX;
    //float VelY;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private bool facingRight = true;
    Rigidbody2D rigBody;
    // Use this for initialization
    void Start () {
        facingRight = true;
        rigBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("Speed", rigBody.velocity.y);

        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);
        Flip(horizontal);
        if ( facingRight) {

            anim.SetFloat("Speed", horizontal * moveSpeed);
        }
        else
        {

            anim.SetFloat("Speed", horizontal * -moveSpeed);
        }
    }
    private void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground",!grounded);
            rigBody.AddForce(new Vector2 (0, jumpForce));
            
        }
    }

    private void HandleMovement(float horizontal) {

        rigBody.velocity = new Vector2(horizontal * moveSpeed, rigBody.velocity.y);
    }
    private void Flip(float horizontal) {
        if (horizontal > 0  && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
        }

    }
}
