using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform SpawnPos;
    public GameObject bullet;
    public Transform player;

    public float speed;
    private float move;
    private float lifeTime = .1f;

    bool leftMouseButtonPressed;
    void Start()
    {
        speed = 5f;
        move = 5f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
        Renderer();
        DestroyBullet();
    }
    void CheckInput(){
    
        leftMouseButtonPressed = Input.GetMouseButton(0);
    }

    private void Renderer(){
        if(leftMouseButtonPressed){
            var position = player.position;
            SpawnPos.transform.Rotate(0,0,0,0);
            SpawnPos.position = position;
            Instantiate(bullet, SpawnPos.position, SpawnPos.rotation);
            moveBullet();
        }
    }

    private void DestroyBullet(){
            if(lifeTime > 0){
            lifeTime -= Time.deltaTime;
        }else{
            Destroy(this.gameObject);
        }
    }

    private void moveBullet(){
        rb.velocity = new Vector2(move * speed, rb.velocity.x);
    }
}
