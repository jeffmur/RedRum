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
	private bool spawnedBoss;
	public GameObject casper;

	void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if(rooms[i].name == "Entry Room"){
					var room = Instantiate(casper, new Vector3(-100,0,-20), Quaternion.identity);
                    room.transform.parent = this.gameObject.transform;
                    spawnedBoss = true;
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	}
}
