using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text text;
    public BulletUI bulletui;
    // Update is called once per frame
    void Update()
    {
        string totalShots = Casper.Instance.localPlayerData.totalShots.ToString();
        string roomsCleared = Casper.Instance.localPlayerData.roomsCleared.ToString();
        string bulletsHit = Casper.Instance.localPlayerData.bulletsHit.ToString();
        string itemsPickedUp = Casper.Instance.localPlayerData.itemsPickedUp.ToString();
        string enemiesKilled = Casper.Instance.localPlayerData.enemiesKilled.ToString();
    text.text = "Total Shots: " + totalShots + '\n' +
            "Rooms Cleared: " + roomsCleared + '\n' +
            "Bullets Hit: " + bulletsHit + '\n' +
            "Items Picked Up: " + itemsPickedUp + '\n' +
            "Enemies Killed: " + enemiesKilled + '\n';
    }
}
