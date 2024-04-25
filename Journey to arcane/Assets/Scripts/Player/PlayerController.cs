using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump System")]
    
    [SerializeField] float jumpHeight;
    [SerializeField] float fallSpeed;
    [SerializeField] float jumpTime;
    [SerializeField] float jumpBuffer;
    Vector2 vecGravity;
  
    //public AudioSource jumpSound;
    //public AudioSource landingSound;
    private float move;
    public float maxSpeed;
    bool facingRight;

    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isJumping;
    float jumpCounter;

    private Rigidbody2D myBody;
    private Animator myAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        myBody = GetComponent<Rigidbody2D> ();
        myAnim = GetComponent<Animator> ();
        
        facingRight = true;
    
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Movement();
        if(move>0 && !facingRight){
            flip();
        }else if (move<0 && facingRight){
            flip();
        }

        if(myBody.velocity.x !=0)
            myAnim.SetBool("isMove", true);
        else
            myAnim.SetBool("isMove", false);

        Jump();
        
    }
    private void CheckInput()
    {
        move = Input.GetAxisRaw("Horizontal");
    }

    private void Movement()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = 14;
            myAnim.SetBool("isRun", true);
        }
        else
        {
            maxSpeed = 8;
            myAnim.SetBool("isRun", false);
        }
        myBody.velocity = new Vector2(move*maxSpeed, myBody.velocity.y);

    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
            isJumping = true;
            jumpCounter = 0;
        }

        if(myBody.velocity.y>0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumpTime)  isJumping = false;

            float t =  jumpCounter / jumpTime;
            float currentJumpB = jumpBuffer;
            if(t>0.5)
            {
                currentJumpB = jumpBuffer * (1-t);
            }

            myBody.velocity += vecGravity * currentJumpB * Time.deltaTime; 
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpCounter =0;
            if(myBody.velocity.y >0)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y * 0.6f);
            }
        }

        if(myBody.velocity.y < 0)
        {
            myBody.velocity -= vecGravity * fallSpeed * Time.deltaTime;
        }
    }

    void flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale; 
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    IEnumerator Co_CoyoteTimer()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        //grounded = false;
    }
}
