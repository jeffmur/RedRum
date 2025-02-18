﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int bulletDamage = 1;
    public float bulletSpeed = 3f;
    public Vector3 bulletDirection;

    protected virtual void Update()
    {
        if (bulletDirection != null)
            transform.localPosition +=  bulletDirection * bulletSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
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
                    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(effect, 0.5f);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetBulletDirection(Vector3 direction)
    {
        bulletDirection = direction.normalized;
    }
}
