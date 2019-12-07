using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int bulletDamage = 1;
    public float bulletSpeed = 3f;
    public Vector3 bulletDirection;

    void Update()
    {
        if (bulletDirection != null)
            transform.localPosition += bulletDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    public void SetBulletDirection(Vector3 direction)
    {
        bulletDirection = direction.normalized;
    }
}
