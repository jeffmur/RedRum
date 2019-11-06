using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject OpenedChest;
    //public GameObject whatToSpawnClone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //spawn the opened Chest
        spawnOpenedChest();
    }
    public void spawnOpenedChest()
    {
        Instantiate(OpenedChest, spawnLocation.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
