using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int bulletDamage = 1;
    public float bulletSpeed = 3f;
    private Vector3 bulletDirection;

    void Update()
    {
        if (bulletDirection != null)
            transform.localPosition +=  bulletDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            other.GetComponent<PlayerStats>().loseHealth(bulletDamage);
            Destroy(gameObject);
        }
    }

    public void SetBulletDirection(Vector3 direction)
    {
        bulletDirection = direction;
    }
}
