/* The following script controls Mario's movement */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask isGroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please set a transform value for groundcheck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector3.up * 300);

            if (Input.GetButton("Jump") && Input.GetButtonDown("Vertical"))
            {
                if (Input.GetButtonDown("Jump") && Input.GetButton("Vertical"))
                {
                    anim.SetBool("jumpAtSwitch", true);
                }
            }
        }

        if (Input.GetButton("Fire1"))       // when the left Ctrl key is pressed, play the attack animation
        {
            anim.SetTrigger("animTrigger");
        }
        

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        anim.SetFloat("moveValue", Mathf.Abs(horizontalInput));  
    }
}
