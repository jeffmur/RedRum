using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SkeletonAbstract : Enemy
{

    private float attackDelay = 1f;
    private float attackRange = 1f;
    private float lastAttackTime;
    private float distanceToPlayer;
    private int counter;
    private Vector2 movement;

    protected override void Start()
    {
        enemyHealth = 150;
        speed = 2f;
        counter = 0;
        base.Start();
        rb = this.GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement = (casper.transform.position - transform.position).normalized;

        distanceToPlayer = Vector2.Distance(casper.transform.position, transform.position);

        if (enemyAnimator.GetBool("Die") == false && distanceToPlayer > attackRange)
        {
            enemyAnimator.SetBool("isAttacking", false);
            Move(movement);
        }
        else
        {
            if (Time.time > (lastAttackTime + attackDelay) && enemyAnimator.GetBool("Die") == false)
            {
                enemyAnimator.SetFloat("Speed", 0);
                enemyAnimator.SetBool("isAttacking", true);
                lastAttackTime = Time.time;
                Attack(1);
            }
        }
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        if (enemyHealth <= 0 && !enemyAnimator.GetBool("Die") && counter < 1)
        {
            counter++;
            enemyAnimator.SetBool("Die", true);
            GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 0.75f);
        }
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

        enemyAnimator.SetFloat("Speed", speed);
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.smoothDeltaTime));
    }

    protected override void Attack(int damage)
    {
        casper.GetComponent<Casper>().changeHealth(-damage);
    }
}
