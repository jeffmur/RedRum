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

    public void initMe(Transform room)
    {
        /** TEMP SOLUTION
         * Creates clones in room at random postion (currently both enemies spawn)
         * Reach Goal: Have ANY enemy or group spawn
         */
        var me = Instantiate(this);
        Health = 125;
        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);
        me.transform.position = new Vector3(room.position.x+x, room.position.y + y, 0);
        me.transform.parent = room.transform;
        me.gameObject.SetActive(true);
    }
}
