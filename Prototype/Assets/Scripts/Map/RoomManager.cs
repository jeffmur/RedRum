using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomManager : MonoBehaviour
{
    public int RoomIndex;
    public List<GameObject> Enemies;
    private DoorSystem sDoorSys;
    public GameObject chestPrefab;
    private Chest myChest;
    bool chestOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        sDoorSys = GetComponent<DoorSystem>();
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

            // If Chest is opened
            if (chestOpen || myChest == null)
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

    public void Initialize(int atIndex)
    {
        RoomIndex = atIndex;
        // Destroy old chest
        foreach(Transform child in gameObject.transform)
        {
            if (child.name == "chest(Clone)")
                Destroy(child.gameObject);
            if (child.tag == "Item")
                child.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Spawn Chest and Hide
        GameObject c = Instantiate(chestPrefab, transform.position, Quaternion.identity);
        c.transform.parent = gameObject.transform;
        myChest = c.GetComponent<Chest>();
        // Hide
        myChest.initChest(atIndex);
        // Random generation of enemies
        // Between 1 and 5 enemies per room
        int AmountOFEnemies = Random.Range(1, 5);
        for (int i = 0; AmountOFEnemies > i; i++) //creates a random amount of enemies
            {
                float x = Random.Range(-4, 4);
                float y = Random.Range(-4, 4);
                int typeOfEnemy = Random.Range(0, Enemies.Count); //number of types of enemies 
                if (Enemies[typeOfEnemy] == null) { typeOfEnemy--; }
                GameObject enemy = Enemies[typeOfEnemy];
                GameObject ChildEnemy = Instantiate(enemy, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
                ChildEnemy.gameObject.SetActive(true);
                ChildEnemy.transform.parent = transform;
            // Boss Room should only spawn one
            if (this.name == "Boss Pool") return; 
            }
    }

    private bool allEnemiesDead()
    {
        if(Enemies.Count == 0) { return true; }

        foreach(Transform child in this.transform)
        {
            if (child.tag == "Enemy")
            {
                sDoorSys.LockAll();
                myChest.gameObject.SetActive(false);
                return false;
            }
        }
        // no enemies
        return true;
    }
}
