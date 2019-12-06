using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : ActivatedItem
{
    public CircleCollider2D reflector;
    public GameObject sphere;

    protected override void Awake()
    {
        base.Awake();
        reflector.enabled = false;
        sphere.SetActive(false);
    }

    protected override void doItemEffect()
    {
        reflector.transform.position = casper.transform.position;
        sphere.transform.position = casper.transform.position;
    }

    protected override void endItemEffect()
    {
        reflector.enabled = false;
        sphere.SetActive(false);
    }

    protected override void setActivateItemBehavior()
    {
        sphere.SetActive(true);
        reflector.enabled = true;
    }

    protected override void setItemDurations()
    {
        effectDuration = 7;
        cooldownDuration = 10;
        frameTick = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "Reflector")
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                ReflectBullet(collision.gameObject);    
            }
        }
    }

    private void ReflectBullet(GameObject obj)
    {
        EnemyBullet bullet = obj.GetComponent<EnemyBullet>();
        Vector3 inDirection = bullet.bulletDirection;
        Vector3 inNormal = casper.transform.position - obj.transform.position;
        bullet.bulletDirection = Vector3.Reflect(inDirection, inNormal);
        bullet.tag = "HeroBullet";
        bullet.bulletDamage = 50;
    }

    protected override void setItemInfo()
    {
        itemName = "Reflector";
        itemID = 7;
        caption = "Benjamin's trusty badge";
    }
}
