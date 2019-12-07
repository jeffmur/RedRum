using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAbstract : Enemy
{
    // Start is called before the first frame update
    
    private bool fireDiagonal;
    private float BulletCooldown = 1f;

    private readonly Vector3 northWest = Vector3.up + Vector3.left;
    private readonly Vector3 northEast = Vector3.up + Vector3.right;
    private readonly Vector3 southWest = Vector3.down + Vector3.left;
    private readonly Vector3 southEast = Vector3.down + Vector3.right;
    protected override void Start()
    {
        base.Start();
        enemyHealth = 2000;

        // CHRIS CHANGE IT TO PURPLE FLAMES :)
        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/SuitcaseBullet_Variant");
        StartCoroutine(FireSlimes());
    }

    private IEnumerator FireSlimes()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            //switch (enemyHealth)
            //{
            //}
            Attack(i);
            i++;
        }
    }

    protected override void Attack(int index)
    {
        nippleShot(100);
        //fourWavesShot(index);
        //bombSpread();
    }

    private void bombSpread()
    {
        //BulletCooldown = 10f;
        EnemyBullet bomb = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bomb.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        bomb.SetBulletDirection(casper.transform.position - transform.position);

        Debug.Log("BOMB HIT THE WALL");
        //bulletSpray(0, 360, 100);

    }

    /**
     * Shoots four massive waves of bullets that rotate 360
     * Degrees based on index (increments by counter in FireSlimes
     */
    private void fourWavesShot(int index)
    {
        BulletCooldown = 0;
        int shots = 5;
        int val = index % 360;
        bulletSpray(val + 270, val + 250, shots, transform.position);
        bulletSpray(val + 180, val + 160, shots, transform.position);
        bulletSpray(val + 90, val + 70, shots, transform.position);
        bulletSpray(val, val - 20, shots, transform.position);
    }

    /**
     * Looks like a nippe ;)
     * Shoots a full circle
     */
    private void nippleShot(int numberOfBullets)
    {
        BulletCooldown = 3f;
        bulletSpray(0, 360, numberOfBullets, transform.position);
    }

    private void bulletSpray(float endAngle, float startAngle, int numOfBullets, Vector3 loc)
    {
        EnemyBullet[] bullets = CreateBullets(numOfBullets);
        float angleStep = (endAngle - startAngle) / bullets.Length;
        float angle = startAngle;
        for (int i = 0; i < bullets.Length; i++)
        {
            float bulDirX = loc.x + Mathf.Sin((angle * Mathf.PI) / 180F);
            float bulDiry = loc.y + Mathf.Cos((angle * Mathf.PI) / 180F);
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDiry, 0f);
            Vector2 bulDir = (bulMoveVector - loc);
            bullets[i].SetBulletDirection(bulDir);
            bullets[i].bulletSpeed = 10f;

            angle += angleStep;
        }
    }
    private EnemyBullet[] CreateBullets(int amt)
    {
        EnemyBullet[] bullets = new EnemyBullet[amt];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        return bullets;
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        if (enemyHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Casper>().changeHealth(-1);
        }
    }
}
