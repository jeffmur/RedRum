using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPos : MonoBehaviour {

    public float waitTime;
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;

	void Update () {

        Collider2D room = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(room == null && levelGen.stopGeneration == true)
        {
            // Fill extra space with random rooms
            int rand = Random.Range(0, levelGen.rooms.Length);
            GameObject r = Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            r.transform.parent = levelGen.extraRooms.transform;
            Destroy(gameObject);
        }
	}
}
