using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : Enemy
{
    private int bulletAmount = 8;

    private float startAngle = 90, endAngle = 280f;

    private float BulletCooldown = 2f;

    private bool lazerTime = false;
    private bool moveRight = true;
    private int shootingTime = 0;

    public Transform firePoint;
    private GameObject spiderBullet;

    private Animator animator;

    public LineRenderer lineRenderer;
    protected override void Start()
    {
        base.Start();
        enemyHealth = 1000;
        speed = 3f;
        spiderBullet = Resources.Load<GameObject>("Textures/Projectiles/SpiderBigBullet Variant");
        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/SpiderBullet");
        animator = transform.GetComponent<Animator>();
        moveToTop();
        StartCoroutine(FireBullets());

        setHealthbarMaxValue(enemyHealth);
    }

    private IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            animator.SetTrigger("Fire");
            if (!lazerTime)
                Attack(-100);
        }
    }


    protected override void Update()
    {

        if (!lazerTime) {
            Move(Vector2.up);
        }

        if (shootingTime == 10)
        {
            animator.SetTrigger("Fire");
            StartCoroutine(shootLaserBreak());
        }
    }
    private void shootLaser(Vector3 fromLoc)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(fromLoc, -Vector3.up);
        if (hitInfo)
        {
            Casper player = hitInfo.transform.GetComponent<Casper>();
            if (player != null)
            {
                Casper.Instance.changeHealth(-1);
            }
        }
        lineRenderer.SetPosition(0, fromLoc);
        lineRenderer.SetPosition(1, fromLoc - Vector3.up * 8);
        lineRenderer.enabled = true;
    }

    private void moveToTop()
    {
        do
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        while (transform.position.y < GetComponentInParent<RoomStats>().MaxY);
    }

    private IEnumerator shootLaserBreak()
    {
        shootLaser(firePoint.position);
        yield return new WaitForSeconds(BulletCooldown);
        lineRenderer.enabled = false;
        shootingTime = 0;
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
        Bullet[] bullets = CreateBullets();
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
    if (shootingTime==5)
        shootSpiderBullet();
    }
    private void shootSpiderBullet()
    {
        if (spiderBullet != null && spiderBullet.GetComponent<SpiderBullet>())
        {
            var bullet = Instantiate(spiderBullet, firePoint.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetBulletDirection(Vector3.down);
        }
    }


    private Bullet[] CreateBullets()
    {
        Bullet[] bullets = new Bullet[bulletAmount];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity).GetComponent<Bullet>();
        }
        return bullets;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Wall"))
        {
            moveRight = !moveRight;
        }
    }
    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        updateHeathBar(damage);
        if (enemyHealth < 500)
        {
            BulletCooldown = 1f;
            bulletAmount = 15;
        }
    }
}
