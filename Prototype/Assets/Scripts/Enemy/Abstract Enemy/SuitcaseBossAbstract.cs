using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitcaseBossAbstract : Enemy
{

    private float BulletCooldown = 3f;
    private Vector3 direction;


    protected override void Start()
    {
        enemyHealth = 1000;
        speed = 1f;

        base.Start();


        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/SuitcaseBullet_Variant");
        enemySprite = transform.GetComponent<SpriteRenderer>();
        StartCoroutine(FireShots());


        setHealthbarMaxValue(enemyHealth);


        }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        direction = Vector3.Normalize(casper.transform.position - transform.position);
        float distance = Vector3.Distance(casper.transform.position, transform.position);
        if (distance <= 15f && distance >= 1)
            transform.position += speed * direction * Time.deltaTime;
        if (direction.x < 0)
        {
            enemySprite.flipX = false;
        }
        else
        {
            enemySprite.flipX = true;
        }

    }
    private IEnumerator FireShots()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            Attack(1);
        }
    }

    protected override void Attack(int damage)
    {
        Bullet[] bullets = CreateBullets(6);
        float spread = 10f;
        for (int i = 0; i < bullets.Length; i++)
        {
            Quaternion angle = Quaternion.FromToRotation(bullets[i].transform.up, direction);
            bullets[i].transform.rotation *= angle;
            bullets[i].transform.rotation = Quaternion.AngleAxis(spread * (i - 1), transform.forward) * bullets[i].transform.rotation;
            bullets[i].SetBulletDirection(bullets[i].transform.up);
            spread *= -1;
        }
    }

    private Bullet[] CreateBullets(int number)
    {
        Bullet[] bullets = new Bullet[number];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        }
        return bullets;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Casper>().changeHealth(-1);
        }
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        updateHeathBar(damage);
        if(enemyHealth < 500)
        {
            enemySprite.color = Color.red;
            speed = 2.5f;
            BulletCooldown = 1f;

        }
    }

}
