using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallShooterAbstract : Enemy
{


    private float shootingCooldown = 2f;
    private float enemyShootingRange = 2f;


    private int currentStates;
    private Vector3 bulletShootDirc;
    private int direction;
    private float timeBtwShot;
    private float freezeTime = 0f;

    private enum States
    {
        Wall,
        TopDownWallAttack,
        SideWallAttack
    }

    protected override void Start()
    {
        enemyHealth = 125;
        speed = 10f;

        base.Start();
        direction = UnityEngine.Random.Range(0, 4);
        currentStates = (int)States.Wall;
        enemyAnimator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/SlimeBullet");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (currentStates == (int)States.Wall)
        {
            WallState();
        }
        else if (currentStates == (int)States.TopDownWallAttack)
        {
            TopDownWallAttackState();
        }
        else if (currentStates == (int)States.SideWallAttack)
        {
            SideWallAttackState();
        }
        timeBtwShot -= Time.deltaTime;
        freezeTime -= Time.deltaTime;
    }

    private void colesCole(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (direction == 0 || direction == 1)
            {
                if (direction == 1)
                {
                    Sprite change = Resources.Load<Sprite>("Textures/Animations/WallShooterEnemy/WallshooterFaceUp");
                    enemyAnimator.SetFloat("State", 1);
                    enemySprite.sprite = change;
                }
                else
                    enemyAnimator.SetFloat("State", 0);
                currentStates = (int)States.TopDownWallAttack;
            }
            else
            {
                Sprite change = Resources.Load<Sprite>("Textures/Animations/WallShooterEnemy/WallshooterFaceRight");
                enemySprite.sprite = change;
                if (direction == 3)
                {
                    enemySprite.flipX = true;
                }

                enemyAnimator.SetFloat("State", 2);
                currentStates = (int)States.SideWallAttack;
            }
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<Casper>().changeHealth(-BulletPrefab.GetComponent<EnemyBullet>().bulletDamage);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        colesCole(other);
    }

    protected void OnTriggerEnter2D(TilemapCollider2D other)
    {
        colesCole(other);
    }
    void WallState()
    {

        //rb.MovePosition(transform.position + transform.up * Time.fixedDeltaTime * 5f);
        switch (direction)
        {
            case 0:
                rb.MovePosition(transform.position + transform.up * Time.fixedDeltaTime * 5f);
                bulletShootDirc = Vector3.down;
                break;
            case 1:
                rb.MovePosition(transform.position - transform.up * Time.fixedDeltaTime * 5f);
                bulletShootDirc = Vector3.up;
                break;
            case 2:
                rb.MovePosition(transform.position - transform.right * Time.fixedDeltaTime * 5f);
                bulletShootDirc = Vector3.right;
                break;
            case 3:
                rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime * 5f);
                bulletShootDirc = Vector3.left;
                break;
        }
    }

    void TopDownWallAttackState()
    {

        Vector3 chase = new Vector3(casper.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector2 dirction = (chase - transform.position).normalized;

        if (Mathf.Abs(this.transform.position.x - casper.transform.position.x) <= enemyShootingRange)
        {
            Attack(1);
        }
        if (freezeTime < 0)
        {
            enemyAnimator.SetBool("Attack", false);
            if (Mathf.Abs(this.transform.position.x - casper.transform.position.x) >= 0.1f)
                rb.MovePosition((Vector2)transform.position + (dirction * speed * Time.smoothDeltaTime));
        }
    }

    void SideWallAttackState()
    {

        Vector3 chase = new Vector3(this.transform.position.x, casper.transform.position.y, this.transform.position.z);
        Vector2 dirction = (chase - transform.position).normalized;

        if (Mathf.Abs(this.transform.position.y - casper.transform.position.y) <= enemyShootingRange)
        {
            Attack(1);
        }
        if (freezeTime < 0)
        {
            enemyAnimator.SetBool("Attack", false);
            if (Mathf.Abs(this.transform.position.y - casper.transform.position.y) >= 0.1f)
                rb.MovePosition((Vector2)transform.position + (dirction * speed * Time.smoothDeltaTime));
        }

    }

    protected override void Attack(int damage)
    {
        if (timeBtwShot < 0)
        {
            enemyAnimator.SetBool("Attack", true);
            EnemyBullet bullets = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullets.SetBulletDirection(bulletShootDirc);
            timeBtwShot = shootingCooldown;
            speed = UnityEngine.Random.Range(2, 8);
            freezeTime = 1f;
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
}
