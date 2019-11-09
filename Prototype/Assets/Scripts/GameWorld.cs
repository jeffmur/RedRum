using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameWorld : MonoBehaviour
{
    private PlayerStats stats;
    public EventManager eventManager;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GameObject.Find("Casper").GetComponent<PlayerStats>();
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
        if (Input.GetKeyDown("1"))
        {
            stats.changeMaxHealth(1);
        }
        if (Input.GetKeyDown("2"))
        {
            stats.changeMaxHealth(-1);
        }
        if (Input.GetKeyDown("3"))
        {
            stats.changeHealth(1);
        }
        if (Input.GetKeyDown("4"))
        {
            stats.changeHealth(-1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (stats.currentHeldItem is ActivatedItem)
            {
                stats.currentHeldItem.useHeldItem();
            }
        }
    }
}
