using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 screenBounds;
    public GameObject hitEffect;

    public int bulletDamage = 20;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect,1f);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("Enemy"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect);
            other.GetComponent<Enemy>().TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
