using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // No enemies and doors are locked
        if(allEnemiesDead() && GetComponent<DoorSystem>().getStatus() == 0)
            GetComponent<DoorSystem>().UnlockAll();
    }

    public void Initialize()
    {
        foreach(GameObject enemy in Enemies)
        {
            enemy.GetComponent<EnemyHealthManager>().initMe(transform);
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
