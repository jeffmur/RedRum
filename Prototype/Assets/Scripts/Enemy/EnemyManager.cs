using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SceneSingleton<EnemyManager>
{
    [SerializeField]
    public List<GameObject> listOfEnemies = null;
    public List<GameObject> listOfBosses = null;
    public GameObject spawnPoint;
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        Object[] enemies = Resources.LoadAll("Textures/Prefabs/Enemies/Level2");
        Object[] bosses = Resources.LoadAll("Textures/Prefabs/Enemies/Bosses");

        foreach (Object t in enemies)
        {
            GameObject enemy = (GameObject)t;
            listOfEnemies.Add(enemy);
        }

        foreach (Object t in bosses)
        {
            GameObject boss = (GameObject)t;
            listOfBosses.Add(boss);
        }
    }
    public GameObject SpawnRandomEnemy()
    {
        if (listOfEnemies.Count > 0)
        {
            int index = Random.Range(0, listOfEnemies.Count);
            GameObject selectedItem = listOfEnemies[index];
            return selectedItem;
        }
        return null;
    }

    public GameObject SpawnFloorBoss()
    {
        switch (Scenes.currentLevel)
        {
            case 0:
            case 1:
            case 3:
                return getBoss("SpiderBoss");
            default:
                return getBoss("SuitcaseBoss");
        }
    }

    private GameObject getBoss(string name)
    {
        for(int i = 0; i < listOfBosses.Count; i++)
        {
            if (listOfBosses[i].name == name)
                return listOfBosses[i];
        }
        return null;
    }

    public GameObject getSpawnPoint()
    {
        return spawnPoint;
    }

    public int NumOfEnemies()
    {
        return listOfEnemies.Count;
    }

    public int NumOfBosses()
    {
        return listOfBosses.Count;
    }
}
