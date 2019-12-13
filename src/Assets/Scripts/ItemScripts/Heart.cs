using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Casper")
        {
            Casper casper = collision.GetComponent<Casper>();
            if (casper.CurrentHealth != casper.MaxHealth)
            {
                collision.GetComponent<Casper>().changeHealth(1);
                Destroy(this.gameObject);
            }
        }
        
    }
}
