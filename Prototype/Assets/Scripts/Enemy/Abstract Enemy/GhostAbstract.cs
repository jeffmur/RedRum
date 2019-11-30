using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAbstract : Enemy
{
    // Start is called before the first frame update
    private float AngerDistance = 2f;
    private float AngerStateTimeLimit = 1f;


    private Vector2 direction;
    private float timeSinceAngerStarted;
    private int state;
    private enum States
    {
        Idle,
        Anger,
        Chase
    }
    protected override void Start()
    {
        enemyHealth = 200;
        speed = 5f;

        base.Start();
        state = (int)States.Idle;
        enemySprite = transform.GetComponent<SpriteRenderer>();
        rb = transform.GetComponent<Rigidbody2D>();
        enemyAnimator = transform.GetComponent<Animator>();
        enemyAnimator.SetFloat("State", (float)States.Idle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (state == (int)States.Idle)
        {
            IdleState();
        }
        else if (state == (int)States.Anger)
        {
            AngerState();
        }
        else if (state == (int)States.Chase)
        {
            ChaseState();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            state = (int)States.Anger;
            enemyAnimator.SetFloat("State", (float)States.Anger);
        }
        else if (other.CompareTag("Player"))
        {
            casper.GetComponent<Casper>().changeHealth(-1);
        }
        else if (other.CompareTag("HeroBullet"))
        {
            Destroy(other.gameObject);
            DecreaseHealth(other.transform.GetComponent<bullet>().bulletDamage);
            GlobalControl.Instance.savedPlayerData.bulletsHit += 1;
            if (state != (int)States.Chase)
            {
                state = (int)States.Anger;
                enemyAnimator.SetFloat("State", (float)States.Anger);
            }
        }
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        if (enemyHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    private void IdleState()
    {
        // "Casper touches ghost"
        if (Vector2.Distance(casper.transform.localPosition, transform.position) <= AngerDistance)
        {
            state = (int)States.Anger;
            enemyAnimator.SetFloat("State", (float)States.Anger);
        }
        else
            enemyAnimator.SetFloat("State", (float)States.Idle);
    }

    private void AngerState()
    {
        if (timeSinceAngerStarted <= -1)
            timeSinceAngerStarted = Time.time;
        else if (Time.time - timeSinceAngerStarted >= AngerStateTimeLimit)
        {
            GetChaseDirection();
            state = (int)States.Chase;
            enemyAnimator.SetFloat("State", (float)States.Chase);
            timeSinceAngerStarted = -1;
        }
    }

    private void ChaseState()
    {
        float distance = Vector3.Distance(casper.transform.position, transform.position);
        if (distance <= 20)
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        else
            state = (int)States.Idle;
    }

    private void GetChaseDirection()
    {
        direction = Vector3.Normalize(casper.transform.localPosition - transform.position);
        if (direction.x < 0)
        {
            enemySprite.flipX = true;
        }
        else
        {
            enemySprite.flipX = false;
        }
    }
}
