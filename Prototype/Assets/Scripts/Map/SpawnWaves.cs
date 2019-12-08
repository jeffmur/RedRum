using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnWaves : MonoBehaviour
{
    public Tilemap spawn;
    private int waveIndex = 3;
    private int[] EnemiesToSpawn = {8, 10, 14};
    private DoorSystem myDoorsSys;
    private RoomStats myStats;
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
        myStats = GetComponent<RoomStats>();
        myStats.setCamLocation();
        StartCoroutine(delayed(0.2f));
    }

    IEnumerator delayed(float time)
    {
        yield return new WaitForSeconds(time);
        spawnWave(0f);
    }

    void spawnWave(float wait)
    {
        if (waveIndex >= EnemiesToSpawn.Length) { return; }
        onNewWave?.Invoke();
        spawnTime = Time.time;
        //Debug.Log("Spawning " + EnemiesToSpawn[waveIndex] + " enemies");
        StartCoroutine(spawnInMap(EnemiesToSpawn[waveIndex], wait));
        message = "Wave "+(waveIndex+1)+" \n \n Surive to win";
        EventManager.Instance.TriggerNotification(message);
    }

    IEnumerator isBossDead()
    {
        // Check if reaper has been killed F || F
        // reaper.active == true
        // || if currently in a waves
        // TODO doesn't work atm
        while (reaper == null || waveIndex < EnemiesToSpawn.Length)
            yield return null;

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
            myDoorsSys.OpenAll();
            once = true;
        }
        else if (allEnemiesDead() && Time.time - spawnTime > 5 && waveIndex < EnemiesToSpawn.Length)
        {
            onWaveComplete?.Invoke();
            waveIndex++;
            spawnWave(2f);
        }
        isBossDead();

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
                StartCoroutine(SpawnEnemy(place));
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

    IEnumerator SpawnEnemy(Vector3 atLoc)
    {
        var circle = Instantiate(EnemyManager.Instance.spawnPoint);
        circle.transform.position = atLoc;
        Destroy(circle, 2f);
        yield return new WaitForSeconds(2f);
        var enemy = Instantiate(EnemyManager.Instance.SpawnRandomEnemy());
        enemy.transform.position = atLoc;
        enemy.transform.parent = transform; // under room prefab
    }
}
