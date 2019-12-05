using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy
{
    // Start is called before the first frame update
    private float fraquency = 5f;
    private float magnitude = 0.1f;
    public bool isMath = false;
    private Vector2 movement;

    protected override void Start()
    {
        base.Start();
        enemyHealth = 1;
        rb = this.GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement = (casper.transform.position - transform.position).normalized;
        Move(movement);
    }

    void Move(Vector2 direction)
    {
        if (direction.x < 0)
        {
            enemySprite.flipX = true;
        }
        else
        {

            enemySprite.flipX = false;
        }

        rb.MovePosition((Vector2)transform.position + 
            (direction * speed * Time.smoothDeltaTime)+
            ((Vector2)transform.up * isSin(isMath)));

    }

    float isSin(bool isCos)
    {
        float sin = Mathf.Sin(Time.time * fraquency) * magnitude;
        float cos = Mathf.Cos(Time.time * fraquency) * magnitude;
        if (!isCos)
            return sin;
        return cos;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            casper.GetComponent<Casper>().changeHealth(-damage);
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