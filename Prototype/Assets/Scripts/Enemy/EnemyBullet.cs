using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int bulletDamage = 1;
    public float bulletSpeed = 3f;
    public Vector3 bulletDirection;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<PlayerStats>().IsEtherial)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
        }
        if (bulletDirection != null)
            transform.localPosition +=  bulletDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            if (tag != "HeroBullet")
            {
                other.GetComponent<PlayerStats>().changeHealth(-bulletDamage);
            }
            Destroy(gameObject);
        }
    }

    public void SetBulletDirection(Vector3 direction)
    {
        bulletDirection = direction.normalized;
    }
}
