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
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        Debug.Assert(stats != null);
    }

    // Update is called once per frame
    void Update()
    {
        TestController();
    }

    public Tuple<int, int> getStartingHealth()
    {
        return Tuple.Create(stats.CurrentHealth, stats.MaxHealth);
    }

    public Tuple<int,int> getStartingAmmo()
    {
        return Tuple.Create(stats.CurrentAmmo, stats.MaxAmmo);
    }

    public void TestController()
    {
        //if (Input.GetKeyDown("1"))
        //{
        //    stats.changeMaxHealth(1);
        //}
        if (Input.GetKeyDown("2"))
        {
            stats.changeHealth(-1);
        }
        if (Input.GetKeyDown("1"))
        {
            stats.changeHealth(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stats.activateItem();
        }
        if (Input.GetKeyDown("3"))
            GameObject.Find("Main Camera").GetComponent<TimeManager>().PermSlow(0.1f);
    }
}
