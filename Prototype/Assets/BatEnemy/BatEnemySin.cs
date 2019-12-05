using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemySin : Enemy
{
    // Start is called before the first frame update
    private float fraquency = 5f;
    private float magnitude = 0.1f;
    private Vector2 movement;

    protected override void Start()
    {
        enemyHealth = 100;
        speed = 2f;
        base.Start();

        rb = this.GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        movement = (casper.transform.position - transform.position).normalized;
        Move(movement);
    }

    protected override void Move(Vector2 direction)
    {
        if (movement.x < 0)
        {
            enemySprite.flipX = true;
        }
        else
        {

            enemySprite.flipX = false;
        }

        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.smoothDeltaTime)+((Vector2)transform.up*Mathf.Sin(Time.time*fraquency)*magnitude));
        //transform.position = ((Vector2)transform.position+direction)*Time.deltaTime*speed+(Vector2)transform.up*Mathf.Sin(Time.time*fraquency)*magnitude;

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            casper.GetComponent<PlayerStats>().changeHealth(-1);
        }
        else if (collision.CompareTag("HeroBullet"))
        {
            if (enemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    }