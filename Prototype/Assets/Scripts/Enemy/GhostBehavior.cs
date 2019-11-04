using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public float AngerDistance = 3;
    public float Speed = 5;
    public float AngerStateTimeLimit = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyHealthManager enemyHealth;
    private GameObject casper;
    private Vector2 direction;
    private float timeSinceAngerStarted;
    private int state;
    private enum States
    {
        Idle,
        Anger,
        Chase
    }

    // Start is called before the first frame update
    void Start()
    {
        state = (int)States.Idle;
        casper = GameObject.Find("Casper");
        rb = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        animator.SetFloat("State", (float)States.Idle);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        enemyHealth = transform.GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            state = (int)States.Anger;
            animator.SetFloat("State", (float)States.Anger);
        }
        else if (other.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<PlayerStats>().loseHealth(1);
        }
        else if (other.CompareTag("HeroBullet"))
        {
            enemyHealth.DecreaseHealth(other.transform.GetComponent<bullet>().bulletDamage);
            state = (int)States.Anger;
            animator.SetFloat("State", (float)States.Anger);
        }
    }

    private void IdleState()
    {
        // "Casper touches ghost"
        if (Vector2.Distance(casper.transform.localPosition, transform.position) <= AngerDistance) 
        {
            state = (int)States.Anger;
            animator.SetFloat("State", (float)States.Anger);
        }
        else
            animator.SetFloat("State", (float)States.Idle);
    }

    private void AngerState()
    {
        if (timeSinceAngerStarted <= -1)
            timeSinceAngerStarted = Time.time;
        else if (Time.time - timeSinceAngerStarted >= AngerStateTimeLimit)
        {
            GetChaseDirection();
            state = (int)States.Chase;
            animator.SetFloat("State", (float)States.Chase);
            timeSinceAngerStarted = -1;
        }
    }

    private void ChaseState()
    {
        float distance = Vector3.Distance(casper.transform.position, transform.position);
        if (distance <= 13)
            rb.MovePosition(rb.position + direction * Speed * Time.fixedDeltaTime);
        else
            state = (int)States.Idle;
    }

    private void GetChaseDirection()
    {
        direction = Vector3.Normalize(casper.transform.localPosition - transform.position);
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
