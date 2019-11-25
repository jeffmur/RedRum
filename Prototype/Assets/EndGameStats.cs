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
    // Start is called before the first frame update
    void Start()
    {
        GlobalControl TotalStats = GlobalControl.Instance;
        shotsFired = GameObject.Find("ShotsFired");
        accuracy = GameObject.Find("Accuracy");
        roomsCompleted = GameObject.Find("RoomsCompleted");
        ItemsPickedUp = GameObject.Find("ItemsPickedUp");
        shotsFired.GetComponent<Text>().text = "SHOTS FIRED: " + TotalStats.savedPlayerData.totalShots.ToString();
        accuracy.GetComponent<Text>().text = "Accuracy: " + (TotalStats.savedPlayerData.totalShots/TotalStats.savedPlayerData.bulletsHit).ToString() + "%";
        roomsCompleted.GetComponent<Text>().text = "RoomsCompleted: " + TotalStats.savedPlayerData.roomsCleared.ToString();
        ItemsPickedUp.GetComponent<Text>().text = "ItemsPicked Up: " + TotalStats.savedPlayerData.itemsPickedUp.ToString();

    }

}
