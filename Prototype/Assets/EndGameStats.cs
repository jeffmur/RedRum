using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour
{
    public Text accuracy;
    public Text roomsCompleted;
    public Text shotsFired;
    public Text ItemsPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        accuracy = GameObject.Find("Accuracy").GetComponent<Text>();
        roomsCompleted = GameObject.Find("RoomsCompleted").GetComponent<Text>();
        shotsFired = GameObject.Find("ShotsFired").GetComponent<Text>();
        ItemsPickedUp = GameObject.Find("ItemsPickedUp").GetComponent<Text>();
        if (GlobalControl.Instance != null)
            showStats();
    }

    void showStats()
    {
        float accuracyCalc = ((float)GlobalControl.Instance.savedPlayerData.bulletsHit/GlobalControl.Instance.savedPlayerData.totalShots) * 100f;
        Debug.Log("Accuracy: " + accuracyCalc);
        shotsFired.text = "SHOTS FIRED: " + GlobalControl.Instance.savedPlayerData.totalShots;
        accuracy.text = "Accuracy: " + (int)accuracyCalc + "%";

        roomsCompleted.text = "RoomsCompleted: " + GlobalControl.Instance.savedPlayerData.roomsCleared;
        ItemsPickedUp.text = "ItemsPicked Up: " + GlobalControl.Instance.savedPlayerData.itemsPickedUp;
    }

}
