using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomManager : MonoBehaviour
{
    private static EnemyManager EM;
    private DoorSystem sDoorSys;
    public GameObject chestPrefab;
    private Chest myChest;
    private BatSpawner myHole;
    private RoomStats myStats;
    bool chestOpen = false;
    public int roomNum = 0;
    public int RoomIndex { get => roomNum; set => roomNum = value; }


    // Start is called before the first frame update
    void Start()
    {
        sDoorSys = GetComponent<DoorSystem>();
        myStats = GetComponent<RoomStats>();
        EM = EnemyManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        // No enemies - UNLOCK
        if (allEnemiesDead())
        {
            sDoorSys.UnlockAll();
            if (myChest != null)
                myChest.gameObject.SetActive(true);

            if (myHole != null)
                myHole.spawn = true;

            // If Chest is opened || no hole or chest
            if (chestOpen || (myChest == null && myHole == null) )
                sDoorSys.OpenAll();
        }
        // Enemies in room - LOCK
        else
        {
            chestOpen = false;
            sDoorSys.LockAll();
        }

        // Room unlocked and Check for chest status
        if (sDoorSys.getStatus() == 1 && myChest != null)
        {
            chestOpen = myChest.openChest;
        }
        
    }

    public void Initialize()
    {
        //RoomIndex = atIndex;
        // Destroy old chest
        foreach(Transform child in gameObject.transform)
        {
            if (child.name == "chest(Clone)")
                Destroy(child.gameObject);
            if (child.tag == "Item")
                child.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Bat Spawner or Treasure Chest???
        handleChest();

        // Random generation of enemies
        if(EM.NumOfEnemies() == 0) { return; }
        // Between 1 and 5 enemies per room
        Tuple<int, int> num = Scenes.getDifficulty();
        int AmountOFEnemies = UnityEngine.Random.Range(num.Item1, num.Item2);
        for (int i = 0; AmountOFEnemies > i; i++) //creates a random amount of enemies
            {
                // ------- ENEMY POSITION ------
                float x = UnityEngine.Random.Range(-4, 4);
                float y = UnityEngine.Random.Range(-4, 4);
                Vector2 loc = new Vector2(transform.position.x + x, transform.position.y + y);
                loc = myStats.spawnEnemyInBounds(loc); // will return if inbounds
                // -----------------------------
                StartCoroutine(SpawnEnemy(loc));
                // Boss Room should only spawn one
                if (this.name == "Boss Pool") return; 
            }
    }
    IEnumerator SpawnEnemy(Vector3 atLoc)
    {
        var circle = Instantiate(EnemyManager.Instance.spawnPoint);
        circle.transform.position = atLoc;
        circle.transform.parent = transform;
        Destroy(circle, 1f);
        yield return new WaitForSeconds(1f);
        GameObject enemy;
        if (name != "Boss Pool")
            enemy = Instantiate(EnemyManager.Instance.SpawnRandomEnemy(), atLoc, Quaternion.identity);
        else
            enemy = Instantiate(EnemyManager.Instance.SpawnFloorBoss(), atLoc, Quaternion.identity);
        // Add Componets
        enemy.AddComponent<RoomRegister>().RoomIndex = roomNum; // assigns item to roomIndex
        enemy.transform.position = atLoc;
        enemy.transform.parent = transform; // under room prefab
    }

    private bool allEnemiesDead()
    {
        if(EM == null) { return true; }

        foreach(Transform child in this.transform)
        {
            if (child.tag == "Enemy")
            {
                sDoorSys.LockAll();
                if(myChest != null)
                    myChest.gameObject.SetActive(false);
                return false;
            }
        }
        // no enemies
        return true;
    }
    // Either be a hole for 
    private void handleChest()
    {
        // Spawn Chest and Hide
        if (chestPrefab != null)
        {
            GameObject c = Instantiate(chestPrefab, transform.position, Quaternion.identity);
            c.transform.parent = gameObject.transform;
            
            c.TryGetComponent(out Chest me);
            if (me != null)
            {
                myChest = me;
                myChest.initChest(roomNum);
            }

            c.TryGetComponent(out BatSpawner b);
            if (b != null)
            {
                myHole = b;
                myHole.begin();
                myHole.RoomIndex = roomNum;
            }
        }
    }
}
