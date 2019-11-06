using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject OpenedChest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //spawn the opened Chest
        spawnOpenedChest();
    }
    public void spawnOpenedChest()
    {
        Instantiate(OpenedChest, spawnLocation.transform.position - new Vector3(0,0,1), Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
