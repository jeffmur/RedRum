using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasperController : MonoBehaviour
{
    public float MoveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
      
        animator.SetFloat("HorizontalDirection", movement.x);
        animator.SetFloat("VerticalDirection", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
    }
}
