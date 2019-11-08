using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Items;
    private DoorSystem sDoorSys;
    // Start is called before the first frame update
    void Start()
    {
        sDoorSys = GetComponent<DoorSystem>();
    }

    // Update is called once per frame
    void Update()
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
                Instantiate(enemy, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
                enemy.gameObject.SetActive(true);
                // Boss Room should only spawn one
                if (this.name == "Boss Pool") { return; }
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
        return true;
    }
}
