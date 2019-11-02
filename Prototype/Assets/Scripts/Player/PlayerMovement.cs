using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject crosshairs;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerStats playerStats;
    Vector2 movement;

    private void Start()
    {
        playerStats = gameObject.GetComponentInChildren<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        crosshairs = GameObject.Find("crossHairs");
    }
    // Update is called once per frame
    private void Update()
    {
        Vector2 direction = Vector3.Normalize(crosshairs.transform.localPosition - transform.localPosition);
        movement.x =  Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("HorizontalDirection", direction.x);
        animator.SetFloat("VerticalDirection", direction.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * playerStats.MoveSpeed * Time.fixedDeltaTime);
    }
}
