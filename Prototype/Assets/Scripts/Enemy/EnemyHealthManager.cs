using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealthManager : MonoBehaviour
{
    public int Health = 125;
    public GameObject itemDrop;


    public void DecreaseHealth(int damagePoints)
    {
        Health -= damagePoints;
        if (Health < 0)
        {
            if (Random.Range(1, 5) > 3 && itemDrop != null)
            {
                Vector3 loc = new Vector3(transform.position.x, transform.position.y, -1);
                var item = Instantiate(itemDrop, loc, Quaternion.identity);
                Destroy(item, 5f);
            }
            if (name != "Skeleton(Clone)")
                Destroy(gameObject);
            //TEMPORARY FOR ALPHA FIX AFTERWARDS
            if (name == "SuitcaseBoss(Clone)")
            {
                string message = "Level 1 Complete! " +
                 "   Press 5 To Begin Again!";
                EventManager.TriggerNotification(message);
            }
        }      
    }
}
