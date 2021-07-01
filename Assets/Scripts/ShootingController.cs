using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    // private Rigidbody2D rb;
    public Transform SpawnPos;
    public GameObject bullet;
    public Transform player;


    bool leftMouseButtonPressed;
    void Start(){

    }

    void Update()
    {
        CheckInput();
        Renderer();
    }
    void CheckInput(){
    
        leftMouseButtonPressed = Input.GetMouseButtonDown(0);
    }

    private void Renderer(){
        if(leftMouseButtonPressed){
            var position = player.position;
            SpawnPos.transform.Rotate(0,0,0,0);
            SpawnPos.position = position;
            Instantiate(bullet, SpawnPos.position, SpawnPos.rotation);
        }
    }

}
