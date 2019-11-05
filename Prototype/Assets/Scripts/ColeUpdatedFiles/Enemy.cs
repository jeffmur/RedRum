using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator enemyAnimator;
    private SpriteRenderer enemySprite;

    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;

    public GameObject BoneDie;

    public float moveSpeed = 5f;
    public int enemyHealth = 100;
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream:Prototype/Assets/Scripts/ColeUpdatedFiles/Enemy.cs
        Vector3 dirction = player.position - transform.position;
        float angle = Mathf.Atan2(dirction.y, dirction.x) * Mathf.Rad2Deg-90f;
        rb.rotation = angle;
        dirction.Normalize();
        movement = dirction;
=======
        
        Vector3 dirction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        movement = dirction;
        
>>>>>>> Stashed changes:Prototype/Assets/Scripts/Enemy/Enemy.cs
    }


    private void FixedUpdate()
    {
      
        enemyMove(movement);
        
    }

    void enemyMove(Vector2 direction)
    {
            if (movement.x < 0)
            {
                enemySprite.flipX = true;
            }
            else
            {
                enemySprite.flipX = false;
            }
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if (distanceToPlayer < attackRange)
            {
                if (Time.time > (lastAttackTime + attackDelay))
                {
                    enemyAnimator.SetBool("isAttacking", true);
                    lastAttackTime = Time.time;
                }
            }
            else
            {

                enemyAnimator.SetBool("isAttacking", false);
                enemyAnimator.SetFloat("Speed", moveSpeed);
                rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.smoothDeltaTime));
            }
    }

    public void TakeDamage(int damage)
    {

        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            enemyAnimator.SetBool("Die", true);
            Die();
        }
    }

    public void Die()
    {
        GameObject diedBone = Instantiate(BoneDie) as GameObject;
        diedBone.transform.position = transform.position;
        Destroy(this.gameObject);
    }
<<<<<<< Updated upstream:Prototype/Assets/Scripts/ColeUpdatedFiles/Enemy.cs
=======

    private void OnTriggerEnter2D(Collider2D collison)
    {
       // enemyAnimator.SetBool("isAttacking", true);
    }
>>>>>>> Stashed changes:Prototype/Assets/Scripts/Enemy/Enemy.cs
}
