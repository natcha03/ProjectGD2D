using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float ammoSpeed = 5f;
    Rigidbody2D rgbd;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * ammoSpeed;
    }

    void Update()
    {
        rgbd.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") 
        {
            Destroy(other.gameObject); 
        }
        Destroy(gameObject); 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

}
