using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : EnemyBullet
{
    private GameObject BulletPrefab;
    int numberOfBullet = 30;

    private void Start()
    {
        bulletSpeed = 3f;
        bulletDamage = 1;

        BulletPrefab = Resources.Load<GameObject>("Textures/Prefabs/Enemies/SlimeBullet");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall")|| other.CompareTag("Player"))
        {
            Destroy(gameObject);
            bulletSpray(0, 360, numberOfBullet, this.transform.position);
        }
    }

    private void bulletSpray(float endAngle, float startAngle, int numOfBullets, Vector3 loc)
    {
        EnemyBullet[] bullets = new EnemyBullet[numOfBullets];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, loc, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        float angleStep = (endAngle - startAngle) / bullets.Length;
        float angle = startAngle;
        for (int i = 0; i < bullets.Length; i++)
        {
            float bulDirX = loc.x + Mathf.Sin((angle * Mathf.PI) / 180F);
            float bulDiry = loc.y + Mathf.Cos((angle * Mathf.PI) / 180F);
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDiry, 0f);
            Vector2 bulDir = (bulMoveVector - loc);
            bullets[i].transform.position = loc;
            bullets[i].SetBulletDirection(bulDir);
            bullets[i].bulletSpeed = 10f;

            angle += angleStep;
        }
    }
}
