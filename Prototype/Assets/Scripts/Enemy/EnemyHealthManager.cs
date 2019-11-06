using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int Health = 125;

    public void DecreaseHealth(int damagePoints)
    {
        Health -= damagePoints;
        if (Health < 0)
            Destroy(this.gameObject);
    }

}
