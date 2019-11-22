using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Items;
    private DoorSystem sDoorSys;
    public GameObject chestPrefab;
    private Chest myChest;


    // Start is called before the first frame update
    void Start()
    {
        sDoorSys = GetComponent<DoorSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // No enemies and doors are locked
        if (allEnemiesDead() && sDoorSys.getStatus() == 0)
        {
            sDoorSys.UnlockAll();
            if (myChest != null)
                myChest.gameObject.SetActive(true);
        }

        // If unlocked
        if (sDoorSys.getStatus() >= 1)
            sDoorSys.OpenAll();

    }

    public void Initialize()
    {
        // Destroy old chest
        foreach(Transform child in gameObject.transform)
        {
            if (child.name == "chest(Clone)")
                Destroy(child.gameObject);
            if (child.tag == "Item")
                Destroy(child.gameObject);
        }

        // Spawn Chest and Hide
        GameObject c = Instantiate(chestPrefab, transform.position, Quaternion.identity);
        c.transform.parent = gameObject.transform;
        myChest = c.GetComponent<Chest>();
        // Hide
        myChest.initChest(Items);

        // Random generation of enemies
        // Between 1 and 5 enemies per room
        int AmountOFEnemies = Random.Range(1, 5);
            for (int i = 0; AmountOFEnemies > i; i++) //creates a random amount of enemies
            {
                float x = Random.Range(-5, 5);
                float y = Random.Range(-5, 5);
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
                GetComponent<DoorSystem>().LockAll();
                myChest.gameObject.SetActive(false);
                return false;
            }
        }
        // no enemies
        return true;
    }
}
