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

    protected GameObject floatingDamage;

    protected virtual void Start()
    {
        casper = GameObject.Find("Casper");
        itemDrop = Resources.Load<GameObject>("Textures/Prefabs/Items/Heart");
        floatingDamage = Resources.Load<GameObject>("UI/floatingDamageStuff/floatingDamage");
    }

    protected virtual void DecreeasHealth(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth < 0)
        {
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
            Destroy(collision.gameObject);
            if(enemyHealth>0)
                ShowFloatingDamage(collision.transform.GetComponent<bullet>().bulletDamage);
            DecreeasHealth(collision.transform.GetComponent<bullet>().bulletDamage);
        }
    }

    private void ShowFloatingDamage(int Damage)
    {
       float a= Random.Range(-0.5f,0.5f);
      Vector2 location = new Vector2(this.transform.position.x+ Random.Range(-0.5f, 0.5f),this.transform.position.y+ Random.Range(1f, 1.5f));
      var floating = Instantiate(floatingDamage, location, Quaternion.identity);
        floating.GetComponent<TMPro.TextMeshPro>().text = Damage.ToString();
        Destroy(floating, 0.5f);
    }
}
