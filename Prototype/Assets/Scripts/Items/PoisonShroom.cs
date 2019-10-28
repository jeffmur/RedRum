using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonShroom : PassiveItem
{
    public override void process()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.localScale = new Vector3(0.7f, 0.7f, 1); //may affect weapon
        player.GetComponent<PlayerStats>().decrementMaxHeath();

    }
}
