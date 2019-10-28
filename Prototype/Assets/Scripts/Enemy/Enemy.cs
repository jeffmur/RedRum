using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;


    public float moveSpeed = 5f;
    public int enemyHealth = 100;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirction = player.position - transform.position;
        float angle = Mathf.Atan2(dirction.y, dirction.x) * Mathf.Rad2Deg-90f;
        rb.rotation = angle;
        dirction.Normalize();
        movement = dirction;
    }


    private void FixedUpdate()
    {
        enemyMove(movement);
    }

    void enemyMove(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.smoothDeltaTime));
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
