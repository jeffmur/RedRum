using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Items;
    public GameObject ClosedChest;
    private bool ChestSpawned;
    private DoorSystem sDoorSys;
    // Start is called before the first frame update
    void Start()
    {
        sDoorSys = GetComponent<DoorSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // No enemies and doors are locked
        if(allEnemiesDead() && sDoorSys.getStatus() == 0)
            sDoorSys.UnlockAll();
        // If unlocked and right mouse clicked
        if (sDoorSys.getStatus() == 1 && Input.GetMouseButtonDown(1))
            sDoorSys.OpenAll();
    }

    public void Initialize()
    {
        int AmountOFEnemies = Random.Range(0, 5);
        for(int i = 0; AmountOFEnemies > i; i++ ) //creates a random amount of enemies
        {
            float x = Random.Range(-5, 5);
            float y = Random.Range(-5, 5);
            int typeOfEnemy = Random.Range(0, 2); //number of types of enemies 
            GameObject enemy  = Enemies[typeOfEnemy];
            Instantiate(enemy, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
            enemy.gameObject.SetActive(true);
        }
    }

    private bool allEnemiesDead()
    {
        foreach(Transform child in this.transform)
        {
            if (child.tag == "Enemy")
            {
                GetComponent<DoorSystem>().LockAll();
                return false;
            }
        }
        // no enemies
        int RandomChest = Random.Range(0,2);
        if (RandomChest == 1 && !ChestSpawned)
        {
            ChestSpawned = true;
            Instantiate(ClosedChest, transform.position, Quaternion.Euler(0, 0, 0));
        }
        return true;
    }
}

