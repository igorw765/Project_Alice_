using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D playerBody;
	
    public float jumpForce = 1000f;
    public float playerSpeed = 10f;
    public float groundCheckRange = 10000f;
    
    public Transform groundChecker;
    public LayerMask groundLayer;
    
    float horizontalMovement;
    bool jumpPressed;
    
    void Start(){
		
		jumpForce = 10f;
		playerSpeed = 1000f;
        
    }

    void Update(){
        
        CheckInput();
        MovePlayer();
    }
    
   
    
    void CheckInput(){
		
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
    
    void MovePlayer(){
		
        var yMovement = playerBody.velocity.y;
        var grounded = IsGrounded();

        if (jumpPressed && grounded){
			
            yMovement += jumpForce;
        }
        
        
        var playerPosition = horizontalMovement * playerSpeed * Time.deltaTime;
        playerBody.velocity = new Vector2(playerPosition, yMovement);
	}
    
    private bool IsGrounded() => Physics2D.OverlapCircle(groundChecker.position, groundCheckRange, groundLayer) != null;

}
