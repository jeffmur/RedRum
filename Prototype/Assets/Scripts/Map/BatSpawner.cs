using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    public bool spawn = false;
    private int numToSpawn = 0;
    private int count = 0;
    private GameObject batFab;
    private float batSpawnDelay = 0.5f;

    public int RoomIndex = 0;

    private void Awake()
    {
        batFab = Resources.Load("Textures/Prefabs/Enemies/BatEnemy") as GameObject;
        gameObject.SetActive(false);
        // SHOW?
        // HANDLE DOORS?
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (float time = 0; time < batSpawnDelay; time += batSpawnDelay)
        {
            Spawn();
            // Wait until the next spawn time.
            yield return new WaitForSeconds(batSpawnDelay);
        }

        // Then switch to spawning advanced meteors for all future spawns.
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(batSpawnDelay);
        }
    }

    public void begin()
    {
        gameObject.SetActive(true);
        spawn = true;
        numToSpawn = Random.Range(4, Scenes.getInt() * 2);
        transform.position += Vector3.up * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (count > numToSpawn)
            Destroy(gameObject);
    }

    void Spawn()
    {
        count++;
        Vector3 loc = new Vector3(transform.position.x + count, transform.position.y, 0);
        var bat = Instantiate(batFab, loc, Quaternion.identity); // Spawn
        bat.transform.parent = transform.parent; // Room Child
        bat.AddComponent<RoomRegister>().RoomIndex = RoomIndex;
        if(Random.Range(0, 2) >= 1)
            bat.GetComponent<BatEnemy>().isMath = true;
    }
}
