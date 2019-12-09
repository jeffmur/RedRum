using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnWaves : MonoBehaviour
{
    public Tilemap spawn;
    private int waveIndex = -1;
    private int[] EnemiesToSpawn = {8, 10, 5};
    private DoorSystem myDoorsSys;
    private string message;
    private bool once = false;
    private float spawnTime = 0;
    public GameObject reaper;

    public delegate void RoomEventDelegate();
    public event RoomEventDelegate onNewWave, onWaveComplete, onBossRoomEnter, onBossDefeated;

    // Start is called before the first frame update
    void Start()
    {
        myDoorsSys = GetComponent<DoorSystem>();
        Camera.main.transform.position = Casper.Instance.transform.position;
        StartCoroutine(delaySpawn(0.02f));
    }

    IEnumerator delaySpawn(float time)
    {
        yield return new WaitForSeconds(time);
        spawnWave(0f);
        onNewWave?.Invoke();
    }

    void spawnWave(float wait)
    {
        if (waveIndex >= EnemiesToSpawn.Length) { return; }
        spawnTime = Time.time;
        //Debug.Log("Spawning " + EnemiesToSpawn[waveIndex] + " enemies");
        StartCoroutine(spawnInMap(EnemiesToSpawn[waveIndex], wait));
        message = "Wave "+(waveIndex+1)+" \n \n Surive to win";
        EventManager.Instance.TriggerNotification(message);
    }

    public void BossDied()
    {
        onBossDefeated?.Invoke();
    }

    IEnumerator waitToLeave()
    {
        // Wait while casper is in Puzzle
        while (!GameObject.Find("Boss Pool").
            GetComponent<RoomStats>().
            isInRoom(Casper.Instance.transform.position))
        {
            yield return null;
        }

        // Spawn enemy
        reaper.SetActive(true);
        // init Boss
        onBossRoomEnter?.Invoke();
        StartCoroutine(reaper.GetComponent<ReaperAbstract>().beginFight());
    }

    void initBoss()
    {
        Debug.Assert(reaper != null);
        myDoorsSys.OpenAll();
        StartCoroutine(waitToLeave());
    }

    // Update is called once per frame
    void Update()
    {
        // Waves complete
        if (waveIndex >= EnemiesToSpawn.Length && !once)
        {
            initBoss();
            onWaveComplete?.Invoke();
            myDoorsSys.OpenAll();
            once = true;
            message = "You've surived this far \n head to any four doors";
            EventManager.Instance.TriggerNotification(message);
        }
        else if (allEnemiesDead() && Time.time - spawnTime > 5 && waveIndex < EnemiesToSpawn.Length)
        {
            waveIndex++;
            spawnWave(2f);
        }          
    }

    private bool allEnemiesDead()
    {
        foreach (Transform obj in this.transform)
            if (obj.CompareTag("Enemy"))
                return false;

        return true;
    }

    IEnumerator spawnInMap(int numOfEnemies, float wait)
    {
        yield return new WaitForSeconds(wait);
        foreach (var pos in spawn.cellBounds.allPositionsWithin)
        {
            Vector3Int localPos = new Vector3Int(pos.x, pos.y, 0); // map cell tile
            Vector3 place = spawn.CellToWorld(localPos); // to real world pos

            // Random Spawn ->> forces as many 'random' continues as possible
            if (Random.Range(1, 350) > numOfEnemies) { continue; }

            if (checkBounds(localPos, numOfEnemies))
            {
                numOfEnemies--;
                StartCoroutine(SpawnEnemy(place, numOfEnemies));
            }
        }
        // make sure all enemies spawn
        if (numOfEnemies > 0)
            StartCoroutine(spawnInMap(numOfEnemies, 0f));
    }

    private bool checkBounds(Vector3Int pos, int num) // easy to read
    {
        // floor tile != overlap outline tile
        return (spawn.HasTile(pos) && num > 0);
    }

    IEnumerator SpawnEnemy(Vector3 atLoc, int i)
    {
        var circle = Instantiate(EnemyManager.Instance.spawnPoint);
        circle.transform.position = atLoc;
        Destroy(circle, 1.5f);
        yield return new WaitForSeconds(1f);
        GameObject enemy;
        // SPAWN BOSS
        if(waveIndex == EnemiesToSpawn.Length - 1 && i < 2)
        {
            enemy = Instantiate(EnemyManager.Instance.SpawnFloorBoss()); // Spawn 2 of them
            enemy.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        // SPAWN ENEMY
        else
            enemy = Instantiate(EnemyManager.Instance.SpawnRandomEnemy());

        enemy.transform.position = atLoc;
        enemy.transform.parent = transform; // under room prefab
    }
}
