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
    
    float horizontalMovement;

    public bool enableShortJump = true;
    bool jumpPressed;
   
    
    void Start(){
		
		jumpForce = 10f;
		playerSpeed = 1000f;
        _renderer = GetComponentInParent<SpriteRenderer>();
        
    }

    void Update(){
        
        CheckInput();
        MovePlayer();
    }
    
   
    
    void CheckInput(){
		
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        Flip();
    }
    
    void MovePlayer(){
		
        var yMovement = playerBody.velocity.y;
        var grounded = IsGrounded();
        bool statement = jumpPressed && playerBody.velocity.y < 0.1f && playerBody.velocity.y > -0.1f;

        if (statement){
			
            yMovement += jumpForce;
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
        }

        if(Input.GetAxis("Horizontal") > 0){
            _renderer.flipX = false;
        }
        
    }
    
    private bool IsGrounded() => Physics2D.OverlapCircle(groundChecker.position, groundCheckRange, groundLayer) != null;

}
