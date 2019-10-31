using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainController : MonoBehaviour
{
    //declare all UI elements and the model
    public UIHealth healthInfo;
    public GameWorld gameWorld;
    public Text roomStatus;

    // Start is called before the first frame update
    void Start()
    {
        //healthInfo.setStartingHealth(gameWorld.getStartingHealth()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRoomStatus(string room)
    {
        roomStatus.text = "Current Room: " + room;
    }
}
