using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer[] spriteRenderer;
    private Animator animator;
    private bool facingRight = true;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        Debug.Log(move.x);

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

        if (move.x < 0 && facingRight == true)
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(move.x > 0 && facingRight == false)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}
