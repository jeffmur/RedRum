using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    public bool isHovering = false;
    Collider2D itemCollision;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEtherial)
        {
            if (collision.gameObject.tag =="EnemyBullet")
            {
                Physics2D.IgnoreCollision(collision, gameObject.GetComponent<Collider2D>());
            }
        }
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Weapon")
        {
            isHovering = true;
            itemCollision = collision;
            if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                string message = "";
                if (collision.gameObject.tag == "Item" && collision.gameObject.name != "Heart(Clone)")
                {
                    message = "Press E to pick up";
                }
                else if(collision.gameObject.tag == "Weapon")
                {
                    message = "Press E to equip";
                }
                GameWorld.Instance.hint.showFloatingText(collision.gameObject.transform.position, message);
            }
        }        
    }
    private void ObtainEquipment(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                pickUpItem(collision.gameObject.GetComponent<Item>());
                GameWorld.Instance.hint.gameObject.SetActive(false);
                isHovering = false;
            }
        }
        if (collision.gameObject.tag == "Weapon")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                weaponInventory.AddWeaponToInventory(collision.gameObject.GetComponent<Weapon>());
                GameWorld.Instance.hint.gameObject.SetActive(false);
                isHovering = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Weapon")
        {
            GameWorld.Instance.hint.gameObject.SetActive(false);
            isHovering = false;
        }
    }
}
