using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float slimeSpeed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Foxie").transform;
    }

    void Update()
    {
        if(target)
        {
            // Vector3 direction = (target.position - transform.position).normalized;
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // rb.rotation = angle;
            rb.velocity = new Vector2(slimeSpeed, 0f);
            moveDirection = target.position;
        }
    }

}
