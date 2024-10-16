using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    
    Rigidbody2D rd;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = 0;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rd.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}