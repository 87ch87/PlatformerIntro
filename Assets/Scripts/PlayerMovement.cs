using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool isGrounded;
    
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {          
        horizontalValue = Input.GetAxis("Horizontal");    
        
        // Flips the sprite to face the direction we are moving it

        if(horizontalValue < 0) 
        {
            FlipSprite(true);
        }
    
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }  

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());

    }

    private void FixedUpdate()
    {
        //For more smooth movement and response

        rgbd.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.velocity.y);
    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        //Adds force vertically making the sprite jump on jump input

        rgbd.AddForce(new Vector2(0, jumpForce));
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);


        //Draws the ray

        Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);

        //Checks if either foot is on ground

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
