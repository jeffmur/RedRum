using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : Enemy
{
    private int bulletAmount = 8;

    private float startAngle = 90, endAngle = 280f;

    private float BulletCooldown = 0.75f;

    private bool lazerTime = false;
    private bool moveRight = true;
    private int shootingTime = 0;

    public Transform firePoint;
    private GameObject spiderBullet;

    public LineRenderer lineRenderer;
    protected override void Start()
    {
        enemyHealth = 1000;
        spiderBullet = Resources.Load<GameObject>("Textures/Prefabs/Enemies/SpiderBullet");
        BulletPrefab = Resources.Load<GameObject>("Textures/Prefabs/Enemies/SlimeBullet");
        StartCoroutine(FireSlimes());
    }

    private IEnumerator FireSlimes()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            if(!lazerTime)
            Attack(1);
        }
    }


    protected override void Update()
    {
        if(!lazerTime)
        Move(Vector2.up);
    }

    protected override void Move(Vector2 direction)
    {
        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    protected override void Attack(int damage)
    {
        EnemyBullet[] bullets = CreateBullets();
        float angleStep = (endAngle - startAngle) / bulletAmount;
        float angle = startAngle;
        for (int i = 0; i < bulletAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180F);
            float bulDiry = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180F);
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDiry, 0f);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;
            bullets[i].SetBulletDirection(bulDir);
            bullets[i].bulletSpeed = 5f;

            angle += angleStep;
        }
        shootingTime++;
        if (shootingTime == 2)
        {
            StartCoroutine(shootLaser(firePoint.position));
        }
        else if (shootingTime==5)
        shootSpiderBullet();
    }
    private void shootSpiderBullet()
    {
        var bullet = Instantiate(spiderBullet, firePoint.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet.SetBulletDirection(Vector3.down);
    }


    private IEnumerator shootLaser(Vector3 fromLoc)
    {
        shootingTime = 0;
        lazerTime = true;
        RaycastHit2D hitInfo = Physics2D.Raycast(fromLoc, -Vector3.up);
        if (hitInfo)
        {
            Casper player = hitInfo.transform.GetComponent<Casper>();
            if (player != null)
            {
                Casper.Instance.changeHealth(-1);
            }
            lineRenderer.SetPosition(0, fromLoc);
            lineRenderer.SetPosition(1, fromLoc - Vector3.up * 8);
        }
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(.1f);
        lineRenderer.enabled = false;
        lazerTime = false;
    }

    private EnemyBullet[] CreateBullets()
    {
        EnemyBullet[] bullets = new EnemyBullet[bulletAmount];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        return bullets;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Wall"))
        {
            moveRight = !moveRight;
        }
    }
    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        if (enemyHealth <500)
        {
            BulletCooldown = 0.5f;
            bulletAmount = 15;
        }
    }
    }
