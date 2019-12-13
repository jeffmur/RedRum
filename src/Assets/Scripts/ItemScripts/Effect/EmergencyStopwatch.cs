using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyStopwatch : PassiveEffect
{
    private CircleCollider2D circle;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        circle = gameObject.AddComponent<CircleCollider2D>();
        circle.isTrigger = true;
        circle.radius = 1.8f;
    }

    protected override void setItemInfo()
    {
        itemName = "Emergency Stopwatch";
        itemID = 6;
        caption = "Do you believe in fate?";
    }

    protected override void Update()
    {
        circle.transform.position = casper.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            Time.timeScale = 0.5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name == "EmergencyStopwatch")
        {
            if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
            {
                Time.timeScale = 1f;
            }
        }
    }

    protected override void AddEffect()
    {
        GameObject obj = new GameObject();
        obj.transform.parent = itemEffectManager.transform;
        obj.name = "EmergencyStopwatch";
        obj.AddComponent<EmergencyStopwatch>();
        obj.GetComponent<EmergencyStopwatch>().enabled = true;
    }
}
