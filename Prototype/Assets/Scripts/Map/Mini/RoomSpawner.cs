using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;

	public float waitTime = 4f;

	void Start(){
		Destroy(gameObject, waitTime); // destroys spawn points
        templates = GameObject.Find("Mini Template").GetComponent<RoomTemplates>();
        //Debug.Log(templates != null);
		Invoke("Spawn", 0.1f);
	}


	void Spawn(){
		if(spawned == false){
            GameObject newRoom = null;
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, -20f);
			if(openingDirection == 1){
				// Need to spawn a room with a BOTTOM door.
				rand = Random.Range(0, templates.bottomRooms.Length);
				newRoom = Instantiate(templates.bottomRooms[rand], newPos, templates.bottomRooms[rand].transform.rotation);
			} else if(openingDirection == 2){
				// Need to spawn a room with a TOP door.
				rand = Random.Range(0, templates.topRooms.Length);
                newRoom = Instantiate(templates.topRooms[rand], newPos, templates.topRooms[rand].transform.rotation);
			} else if(openingDirection == 3){
				// Need to spawn a room with a LEFT door.
				rand = Random.Range(0, templates.leftRooms.Length);
                newRoom = Instantiate(templates.leftRooms[rand], newPos, templates.leftRooms[rand].transform.rotation);
			} else if(openingDirection == 4){
				// Need to spawn a room with a RIGHT door.
				rand = Random.Range(0, templates.rightRooms.Length);
                newRoom = Instantiate(templates.rightRooms[rand], newPos, templates.rightRooms[rand].transform.rotation);
			}
            if(newRoom != null)
                newRoom.transform.parent = templates.transform;
			spawned = true;
		}
	}
    /*
     * Called every time SpawnPoints Collide
     */
	private void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                var room = Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                room.transform.parent = this.gameObject.transform;
            }
            Destroy(gameObject);
        }
        spawned = true;
    }
}
