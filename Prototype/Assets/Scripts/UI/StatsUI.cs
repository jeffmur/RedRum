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
        // GET CURRENT WEAPON
        Weapon curGun = Casper.Instance.weaponInventory.GetSelectedWeapon();
        string damage = curGun.Damage.ToString();
        string accuracy = curGun.Accuracy.ToString();
        string reloadSp = curGun.reloadSpeed.ToString();
        // 
        double totalShots = Casper.Instance.localPlayerData.totalShots;
        double bulletsHit = Casper.Instance.localPlayerData.bulletsHit;
        string speed = Casper.Instance.localCasperData.Speed.ToString();
        string roomsCleared = Casper.Instance.localPlayerData.roomsCleared.ToString();
        string itemsPickedUp = Casper.Instance.localPlayerData.itemsPickedUp.ToString();
        string enemiesKilled = Casper.Instance.localPlayerData.enemiesKilled.ToString();
        
    text.text = "<color=orange><b><i>Equipped</i></b></color> " + curGun.name + "\n"+
                "\t <color=red>Damage:</color> " + damage + "\n" +
                "\t <color=yellow>Accuracy:</color> + " + accuracy + "% \n" +
                "\t <color=green>Reload:</color> " + reloadSp + " sec\n" +
                "<color=orange><b><i>Casper</i></b> Speed:</color> " + speed + "\n" +
                "Rooms <color=green>Cleared</color>: " + roomsCleared + '\n' +
                "Items <color=yellow>Picked Up:</color> " + itemsPickedUp + '\n' +
                "Enemies <color=red>Killed:</color> " + enemiesKilled;
                
    }
}
