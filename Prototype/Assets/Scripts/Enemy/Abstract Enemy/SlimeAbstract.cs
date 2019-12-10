using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAbstract : Enemy
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
        enemyHealth = 150;

        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/SlimeBullet");
        StartCoroutine(FireSlimes());
    }

    private IEnumerator FireSlimes()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            Attack(1);
        }
    }

    protected override void Attack(int damage)
    {
        fireInterchange();
        if (enemyHealth <= 50)
        {
            BulletCooldown = 0.5f;
            fireInAllDirections();
        }
            
    }

    private void fireInterchange()
    {
        Bullet[] bullets = CreateBullets(4);
        if (!fireDiagonal)
        {
            bullets[0].SetBulletDirection(Vector3.up);
            bullets[1].SetBulletDirection(Vector3.right);
            bullets[2].SetBulletDirection(Vector3.left);
            bullets[3].SetBulletDirection(Vector3.down);
            fireDiagonal = true;
        }
        else
        {
            bullets[0].SetBulletDirection(northWest);
            bullets[1].SetBulletDirection(northEast);
            bullets[2].SetBulletDirection(southWest);
            bullets[3].SetBulletDirection(southEast);
            fireDiagonal = false;
        }
    }
    private void fireInAllDirections()
    {
        Bullet[] bullets = CreateBullets(8);
        // 4 dirs
        bullets[0].SetBulletDirection(Vector3.up);
        bullets[1].SetBulletDirection(Vector3.right);
        bullets[2].SetBulletDirection(Vector3.left);
        bullets[3].SetBulletDirection(Vector3.down);
        // Diagonal
        bullets[4].SetBulletDirection(northWest);
        bullets[5].SetBulletDirection(northEast);
        bullets[6].SetBulletDirection(southWest);
        bullets[7].SetBulletDirection(southEast);
    }

    private Bullet[] CreateBullets(int num)
    {
        Bullet[] bullets = new Bullet[num];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        }
        return bullets;
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
