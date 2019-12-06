using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : SceneSingleton<EnemyManager>
{
    [SerializeField]
    public List<GameObject> listOfEnemies = null;
    public List<GameObject> listOfBosses = null;
    public Tilemap floor;
    public Tilemap outline;

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
        spawnInMap(10);
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

    public void spawnInMap(int numOfEnemies)
    {
        foreach(var pos in floor.cellBounds.allPositionsWithin)
        {
            Vector3Int localPos = new Vector3Int(pos.x, pos.y, 0);
            Vector3 place = floor.CellToWorld(localPos);

            // Random Spawn
            if (Random.Range(1, 990) > numOfEnemies) { continue; }

            if (checkBounds(localPos, numOfEnemies))
            {
                numOfEnemies--;
                var enemy = Instantiate(SpawnRandomEnemy());
                enemy.transform.position = place;

            }
        }
        if (numOfEnemies > 0)
            spawnInMap(numOfEnemies);
    }

    private bool checkBounds(Vector3Int pos, int num)
    {
        return (floor.HasTile(pos) && !outline.HasTile(pos) && num > 0);
    }
}
