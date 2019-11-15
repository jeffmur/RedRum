using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int Health = 125;
    private Object heartDrop;

    private void Start()
    {
        heartDrop = Resources.Load("Items/pickUpHeart.prefab");
    }

    public void DecreaseHealth(int damagePoints)
    {
        Health -= damagePoints;
        if (Health < 0)
        {
            Instantiate(heartDrop, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
           
    }
}
