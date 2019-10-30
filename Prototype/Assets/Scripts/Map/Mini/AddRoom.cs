using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddRoom : MonoBehaviour {

	private RoomTemplates templates;
    private Object prefab;

	void Start(){
        prefab = Resources.Load("MiniMap/Prefabs/"+name);
        templates = GameObject.Find("Mini Template").GetComponent<RoomTemplates>();
        templates.rooms.Add((GameObject)prefab);
	}
}
