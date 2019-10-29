using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RandomPointer : RoomPointer
{
    private GameObject[] allRooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public GameObject[] findCompatible(string doorName)
    {
        GameObject[] options = new GameObject[allRooms.Length/4];
        for(int i = 0; i < allRooms.Length; i++)
        {
            // if (allRooms[i].name == doorName)
            // Add to list
        }
        return options;
    }
}
