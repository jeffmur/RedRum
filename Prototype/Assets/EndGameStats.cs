using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour
{

    public GameObject accuracy;
    public GameObject roomsCompleted;
    public GameObject shotsFired;
    public GameObject ItemsPickedUp;
    public GameObject EnemiesKilled;
    // Start is called before the first frame update
    void Start()
    {

        GlobalControl TotalStats = GlobalControl.Instance;
        shotsFired = GameObject.Find("ShotsFired");
        accuracy = GameObject.Find("Accuracy");
        roomsCompleted = GameObject.Find("RoomsCompleted");
        ItemsPickedUp = GameObject.Find("ItemsPickedUp");
        EnemiesKilled = GameObject.Find("EnemiesKilled");
        shotsFired.GetComponent<Text>().text = "SHOTS FIRED: " + TotalStats.savedPlayerData.totalShots.ToString();
        accuracy.GetComponent<Text>().text = "Accuracy: " + ((TotalStats.savedPlayerData.bulletsHit/TotalStats.savedPlayerData.totalShots)*100).ToString() + "%";
        roomsCompleted.GetComponent<Text>().text = "Rooms Completed: " + TotalStats.savedPlayerData.roomsCleared.ToString();
        ItemsPickedUp.GetComponent<Text>().text = "Items Picked Up: " + TotalStats.savedPlayerData.itemsPickedUp.ToString();
        EnemiesKilled.GetComponent<Text>().text = "Enemies Killed: " + TotalStats.savedPlayerData.enemiesKilled.ToString();


        //roomsCompleted.text = "RoomsCompleted: " + GlobalControl.Instance.savedPlayerData.roomsCleared;
        //ItemsPickedUp.text = "ItemsPicked Up: " + GlobalControl.Instance.savedPlayerData.itemsPickedUp;
    }

}
