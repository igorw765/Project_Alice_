using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController player;

    public float speed;
    private float move;
    public float lifeTime = 1f;
    private bool direction;

    void Start(){
        
        speed = 5f;
        move = 5f;
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();
        direction = PlayerController.isFlipped;
    }

    void Update(){

        Renderer();
    }

    private void Renderer(){
        MoveBullet();
        DestroyBullet();
    }

    private void DestroyBullet(){
            if(lifeTime > 0){
            lifeTime -= Time.deltaTime;
        }else{
            Destroy(this.gameObject);
        }
    }

    private void MoveBullet(){
        if(direction){
            rb.velocity = new Vector2(-move * speed, rb.velocity.x);
        }else{
            rb.velocity = new Vector2(move * speed, rb.velocity.x);
        }
        
    }
}
