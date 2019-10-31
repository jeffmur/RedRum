using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddRoom : MonoBehaviour {

	private RoomTemplates templates;

	void Start(){
        templates = GameObject.Find("Mini Template").GetComponent<RoomTemplates>();
        templates.rooms.Add(gameObject);
	}
}
