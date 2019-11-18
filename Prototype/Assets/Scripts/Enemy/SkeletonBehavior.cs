﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform player;


    public float attackRange = 1;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;

    private float myHealth;


    public float moveSpeed = 5f;
    private float distanceToPlayer;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator enemyAnimator;
    private SpriteRenderer enemySprite;
    private Transform casper;




    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        casper = GameObject.FindGameObjectWithTag("Player").transform;
        myHealth = GetComponent<EnemyHealthManager>().Health;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 dirction = (casper.position - transform.position).normalized;
        movement = dirction;

        distanceToPlayer = Vector2.Distance(casper.transform.position, transform.position);

        if (enemyAnimator.GetBool("Die") == false && distanceToPlayer > attackRange)
        {
            enemyAnimator.SetBool("isAttacking", false);
            EnemyMove(movement);
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


    void EnemyMove(Vector2 direction)
    {

        if (movement.x < 0)
        {
            enemySprite.flipX = true;
        }
        else
        {
            enemySprite.flipX = false;
        }

        enemyAnimator.SetFloat("Speed", moveSpeed);
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.smoothDeltaTime));

    }

    private void takeDamage(int hit)
    {
        myHealth -= hit;
        if (myHealth <= 0)
        {
            enemyAnimator.SetBool("Die", true);
            Destroy(gameObject, 2f);
        }
    }

    private void Attack(int hitDamage)
    {
        casper.GetComponent<PlayerStats>().changeHealth(-hitDamage);
    }



    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("HeroBullet"))
        {
            Destroy(collison.gameObject);
            takeDamage(collison.transform.GetComponent<bullet>().bulletDamage);
        }
    }
}
