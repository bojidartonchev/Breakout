using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : PhysicsObject
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        // If the input is moving the player right and the player is facing left...
        if (move.x > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move.x < 0 && facingRight)
            // ... flip the player.
            Flip();

        //animator.SetBool("grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(move.x));

        targetVelocity = move * maxSpeed;
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
}