using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShooterBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentStates;
    private GameObject casper;

    public GameObject BulletPrefab;
    private Vector3 bulletShootDirc;

    private Rigidbody2D rb;
    private int direction;

    private float timeBtwShot;
    public float shootingCooldown = 2f;
    public float enemyShootingRange = 2f;
    public float moveSpeed = 2f;
    private float myHealth;

    private enum States
    {
        Wall,
        TopDownWallAttack,
        SideWallAttack

    }
    void Start()
    {
        currentStates = (int)States.Wall;
        rb = this.GetComponent<Rigidbody2D>();
        casper = GameObject.Find("Casper");
        direction = Random.Range(0, 4);
        myHealth = GetComponent<EnemyHealthManager>().Health;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentStates == (int)States.Wall)
        {
            WallState();
        }
        else if(currentStates == (int)States.TopDownWallAttack)
        {
            TopDownWallAttackState();
        }
        else if(currentStates == (int)States.SideWallAttack)
        {
            SideWallAttackState();
        }
        timeBtwShot -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Wall"))
        {
            if (direction == 0 || direction == 1)
            {
                currentStates = (int)States.TopDownWallAttack;
            }
            else
            {
                currentStates = (int)States.SideWallAttack;
            }
        }
        else if (collison.CompareTag("Player"))
        {
            collison.GetComponent<PlayerStats>().changeHealth(BulletPrefab.GetComponent<EnemyBullet>().bulletDamage);
        }
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
            Shooting();


        rb.MovePosition((Vector2)transform.position + (dirction * moveSpeed * Time.smoothDeltaTime));
    }


    void SideWallAttackState()
    {
        Vector3 chase = new Vector3(this.transform.position.x, casper.transform.position.y, this.transform.position.z);
        Vector2 dirction = (chase - transform.position).normalized;

          if(Mathf.Abs(this.transform.position.y - casper.transform.position.y)<= enemyShootingRange)
            Shooting();


        rb.MovePosition((Vector2)transform.position + (dirction * moveSpeed * Time.smoothDeltaTime));
    }

    void Shooting()
    {
        if (timeBtwShot < 0)
        {
            EnemyBullet bullets = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullets.SetBulletDirection(bulletShootDirc);
            timeBtwShot = shootingCooldown;
            moveSpeed = Random.Range(2, 8);
        }
    }

}
