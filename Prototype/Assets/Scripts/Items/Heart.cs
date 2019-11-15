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
            collision.GetComponent<PlayerStats>().changeHealth(1);
            Destroy(this.gameObject);
        }
        
    }
}
