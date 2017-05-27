﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.


    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.

    private bool grounded = true;          // Whether or not the player is grounded.
    private Animator anim;                  // Reference to the player's animator component.
    private Rigidbody2D rigidBody;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }
    

    void FixedUpdate()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * rigidBody.velocity.x < maxSpeed)
            // ... add a force to the player.
            rigidBody.AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        // If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            //anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            rigidBody.AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void MoveRight()
    {
        rigidBody.AddForce(rigidBody.velocity * Time.deltaTime, ForceMode2D.Impulse);
        //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        //transform.Translate(velocity);
    }
}