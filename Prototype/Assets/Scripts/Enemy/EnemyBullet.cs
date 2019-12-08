﻿using System.Collections;
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
        if (bulletDirection != null)
            transform.localPosition +=  bulletDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Casper casper = other.GetComponent<Casper>();
            if (!casper.IsEtherial)
            {
                if (tag != "HeroBullet")
                {
                    casper.changeHealth(-bulletDamage);
                }
                
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    public void SetBulletDirection(Vector3 direction)
    {
        bulletDirection = direction.normalized;
    }
}
