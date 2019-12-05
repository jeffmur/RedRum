using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitcaseBossAbstract : Enemy
{

    private float BulletCooldown = 3f;
    private Vector3 direction;
    private GameObject healthBar;

    protected override void Start()
    {
        enemyHealth = 500;
        speed = 1f;

        base.Start();
        healthBar = GameObject.Find("HealthBarSlider");
        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/SuitcaseBullet_Variant");
        enemySprite = transform.GetComponent<SpriteRenderer>();
        StartCoroutine(FireShots());
        healthBar.SetActive(true);
        healthBar.GetComponent<Slider>().maxValue = enemyHealth;


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
        healthBar.GetComponent<Slider>().value = enemyHealth;
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
        EnemyBullet[] bullets = CreateBullets(6);
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

    private EnemyBullet[] CreateBullets(int number)
    {
        EnemyBullet[] bullets = new EnemyBullet[number];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        return bullets;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Casper>().changeHealth(-1);
        }
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        if (enemyHealth < 0)
        {
            Destroy(gameObject);
        }
        else if(enemyHealth< (healthBar.GetComponent<Slider>().maxValue / 2))
        {
            enemySprite.color = Color.red;
            speed = 2f;
            BulletCooldown = 1.5f;

        }
    }

}
