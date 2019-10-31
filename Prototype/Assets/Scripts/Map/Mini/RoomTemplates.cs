using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

    public GameObject closedRoom;
	public List<GameObject> rooms;

	public float waitTime;
	private bool spawnedHero;
    private bool spawnedBoss;
	public GameObject casper;
    public GameObject boss;

    void Start()
    {
        Debug.Assert(boss != null);
        Debug.Assert(casper != null);
    }

    private int getRandomInt()
    {
        return Random.Range(1, rooms.Count);
    }
    void Update(){
        // spawns Hero
        if (waitTime <= 0 && spawnedHero == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].name == "Entry Room")
                {
                    var room = Instantiate(casper, new Vector3(-100, 0, -20), Quaternion.identity);
                    room.transform.parent = this.gameObject.transform;
                    spawnedHero = true;
                }
            }
        }
        // spawns boss
        if (waitTime <= -2f && spawnedBoss == false)
        {
            int randIndex = getRandomInt();
            for (int i = 0; i < rooms.Count; i++)
            {
                if(i == rooms.Count-1)
                {
                    var room = Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    room.transform.parent = this.gameObject.transform;
                    spawnedBoss = true;
                }
                else
                {
                    randIndex = getRandomInt();
                }
            }
        }
        else { waitTime -= Time.deltaTime; }
	}
}
