using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private PlayerStats playerStats;
    Vector2 movement;

    private void Start()
    {
        playerStats = gameObject.GetComponentInChildren<PlayerStats>();
        Debug.Assert(playerStats != null);
    }
    // Update is called once per frame
    private void Update()
    {
       movement.x =  Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * playerStats.MoveSpeed * Time.fixedDeltaTime);
    }
}
