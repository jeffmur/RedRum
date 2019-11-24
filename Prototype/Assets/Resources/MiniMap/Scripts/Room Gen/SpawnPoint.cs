using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject[] objectsToSpawn;
    bool spawnedRoom;

    private void Start()
    {
        if (objectsToSpawn[0].tag == "Rooms")
            spawnedRoom = true;

        GameObject instance = Instantiate(objectsToSpawn[0], transform.position, Quaternion.identity);

        if(!spawnedRoom)
            instance.transform.parent = transform;
    }
}
