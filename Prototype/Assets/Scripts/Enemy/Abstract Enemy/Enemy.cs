using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameObject casper;
    protected int enemyHealth = 100;
    protected GameObject BulletPrefab;
    protected int damage = 1;
    protected GameObject itemDrop;
    protected float speed = 2f;
    protected SpriteRenderer enemySprite;
    protected Animator enemyAnimator;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        casper = GameObject.Find("Casper");
        itemDrop = Resources.Load<GameObject>("Textures/Prefabs/Items/Heart");
    }

    protected virtual void DecreeasHealth(int damage)
    {
        enemyHealth -= damage;
        GlobalControl.Instance.savedPlayerData.bulletsHit += 1; // increasing for stats
        if (enemyHealth < 0)
        {
            GlobalControl.Instance.savedPlayerData.enemiesKilled += 1;
            Debug.Log("Enemies killed " + GlobalControl.Instance.savedPlayerData.enemiesKilled);
            if (Random.Range(1, 5) > 3 && itemDrop != null)
            {
                Vector3 loc = new Vector3(transform.position.x, transform.position.y, -1);
                var item = Instantiate(itemDrop, loc, Quaternion.identity);
                Destroy(item, 5f);
            }
        }
    }

    protected virtual void Attack(int damage)
    {

    }


    protected virtual void Move(Vector2 direction)
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroBullet"))
        {
            GlobalControl.Instance.savedPlayerData.bulletsHit += 1;
            Destroy(collision.gameObject);
            DecreeasHealth(collision.transform.GetComponent<bullet>().bulletDamage);
        }
    }
}
