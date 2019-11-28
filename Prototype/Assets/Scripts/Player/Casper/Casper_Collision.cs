using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper : MonoBehaviour
{
    public ItemFloatingText hint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEtherial)
        {
            if (collision.gameObject.tag =="EnemyBullet")
            {
                Physics2D.IgnoreCollision(collision, gameObject.GetComponent<Collider2D>());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                string message = "Press E to pick up";
                hint.showFloatingText(collision.gameObject.transform.position, message);
            }
        }
        if (collision.gameObject.tag == "Weapon")
        {
            //if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                string message = "Press E to equip";
                hint.showFloatingText(collision.gameObject.transform.position, message);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "Item")
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>())
                {
                    pickUpItem(collision.gameObject.GetComponent<Item>());
                    hint.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Weapon")
        {
            hint.gameObject.SetActive(false);
        }
    }
}
