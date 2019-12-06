using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameObject casper;
    public int enemyHealth = 100;
    protected GameObject BulletPrefab;
    protected int damage = 1;
    protected GameObject itemDrop;
    protected float speed = 2f;
    protected SpriteRenderer enemySprite;
    protected Animator enemyAnimator;
    protected Rigidbody2D rb;
    private Chest myChest;
    protected GameObject floatingDamage;

    protected virtual void Start()
    {
        casper = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(casper != null);
        itemDrop = Resources.Load<GameObject>("Textures/Prefabs/Items/Heart");
        floatingDamage = Resources.Load<GameObject>("UI/hitmarker");
        enemySprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void DecreaseHealth(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth < 0)
        {
            GlobalControl.Instance.savedPlayerData.enemiesKilled += 1;
            if (Random.Range(1, 5) > 3 && itemDrop != null) 
            {
                Vector3 loc = new Vector3(transform.position.x, transform.position.y, -1);
                var item = Instantiate(itemDrop, loc, Quaternion.identity);
                item.AddComponent<RoomRegister>().RoomIndex = GetComponent<RoomRegister>().RoomIndex;
                item.transform.parent = transform.parent; // add to room as child
                item.tag = "Item"; 
            }
        }
    }

    protected virtual void Update()
    {
        Collider2D playerCol = casper.GetComponent<Collider2D>();
        Collider2D enemyCol = GetComponent<Collider2D>();
        Debug.Assert(playerCol != null);
        Debug.Assert(enemyCol != null);
        Physics2D.IgnoreCollision(playerCol, enemyCol, casper.GetComponent<Casper>().IsEtherial);
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
            if (enemyHealth > 0)
                ShowFloatingDamage(collision.transform.GetComponent<bullet>().bulletDamage);

            if (collision.transform.GetComponent<bullet>())
                DecreaseHealth(collision.transform.GetComponent<bullet>().bulletDamage);
            //why can't all bullets be the same, just with different tags?
            if (collision.transform.GetComponent<EnemyBullet>())
            {
                DecreaseHealth(collision.transform.GetComponent<EnemyBullet>().bulletDamage);
            }
        }
    }
    private void ShowFloatingDamage(int Damage)
    {
        float a = Random.Range(-0.5f, 0.5f);
        Vector2 location = new Vector2(this.transform.position.x + Random.Range(-0.5f, 0.5f), this.transform.position.y + Random.Range(1f, 1.5f));
        var floating = Instantiate(floatingDamage, location, Quaternion.identity);
        floating.GetComponent<TMPro.TextMeshPro>().text = Damage.ToString();
        Destroy(floating, 0.5f);
    }
}
