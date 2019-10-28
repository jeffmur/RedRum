using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private PlayerStats playerStats;
    Vector2 movement;
<<<<<<< HEAD:Prototype/Assets/Scripts/Player/PlayerMovement.cs

    private void Start()
    {
        playerStats = gameObject.GetComponentInChildren<PlayerStats>();
        Debug.Assert(playerStats != null);
=======
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
>>>>>>> master:Prototype/Assets/Scripts/Player/PlayerMovement.cs
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
