using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 1000f;
    public float fallMultiplier;
    public float lowJumpFallMultiplier;
    public float playerSpeed = 10f;
    public float groundCheckRange = .1f;
    
    public Transform groundChecker;
    public Rigidbody2D playerBody;
    public LayerMask groundLayer;
    private SpriteRenderer _renderer;
    private Animator _animator;
    
    float horizontalMovement;

    public bool enableShortJump = true;
    bool jumpPressed;
    bool jumpDown;
    bool leftMouseButtonPressed;
    public static bool isFlipped;

   

    void Start(){
		
        isFlipped = false;
		jumpForce = 10f;
		playerSpeed = 1000f;
        _renderer = GetComponentInParent<SpriteRenderer>();
        _animator = GetComponentInParent<Animator>();
    }

    void Update(){
        
        CheckInput();
        MovePlayer();
    }
    
   
    
    void CheckInput(){
		
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        jumpDown = Input.GetKey(KeyCode.Space);
        leftMouseButtonPressed = Input.GetMouseButton(0);
        Flip();
    }
    
    void MovePlayer(){
		
        var yMovement = playerBody.velocity.y;
        var grounded = IsGrounded();
        bool statement = jumpPressed && playerBody.velocity.y < 0.1f && playerBody.velocity.y > -0.1f;

        if (statement){
			
            yMovement += jumpForce;
        }

        if(jumpDown && playerBody.velocity.y > 0.1f){
            _animator.SetBool("jumping", true);
            _animator.SetBool("grounded", false);
        }else{
            _animator.SetBool("jumping", false);
            _animator.SetBool("grounded", true);
        }



        var playerPosition = horizontalMovement * playerSpeed * Time.deltaTime;
        playerBody.velocity = new Vector2(playerPosition, yMovement);
	}


    void BetterFall()
    {
        if (playerBody.velocity.y < 0 && enableShortJump)
        {
            playerBody.velocity += Vector2.up * Physics2D.gravity * fallMultiplier * Time.deltaTime;
        }

        else if (playerBody.velocity.y > 0 && !jumpPressed)
        {
            playerBody.velocity += Vector2.up * Physics2D.gravity * lowJumpFallMultiplier * Time.deltaTime;

        }
    }

    private void Flip(){

        if(Input.GetAxis("Horizontal") < 0){
            _renderer.flipX = true;
            isFlipped = true;
        }

        if(Input.GetAxis("Horizontal") > 0){
            _renderer.flipX = false;
            isFlipped = false;
        }
        
    }
    // public bool IsFlipped(){

    //     if(Input.GetAxis("Horizontal") < 0){
    //         return true;
    //     }else{
    //          return false;
    //     }

    //     // if(Input.GetAxis("Horizontal") > 0){
    //     //     return false;
    //     // }
        
    // }
    
    private bool IsGrounded() => Physics2D.OverlapCircle(groundChecker.position, groundCheckRange, groundLayer) != null;

}
