using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    private GameObject player;
    private PlayerStats stats;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        Debug.Assert(player != null);
        Debug.Assert(stats != null);
    }

    // Update is called once per frame
    void Update()
    {
        testHP();
    }

    public int getStartingHealth()
    {
        return stats.MaxHealth;
    }

    public void testHP()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stats.incrementMaxHeath();
        }
        if (Input.GetMouseButtonDown(1))
        {
            stats.decrementMaxHeath();
        }
        if (Input.GetKeyDown("1"))
        {
            stats.gainHealth(1);
        }
        if (Input.GetKeyDown("2"))
        {
            stats.loseHealth(1);
        }
    }
}
