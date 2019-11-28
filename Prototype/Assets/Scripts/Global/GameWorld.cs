using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameWorld : MonoBehaviour
{
    private Casper casper;
    public EventManager eventManager;

    // Start is called before the first frame update
    void Awake()
    {
        casper = GameObject.Find("Casper").GetComponent<Casper>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        Debug.Assert(casper != null);
    }

    // Update is called once per frame
    void Update()
    {
        TestController();
    }

    public Tuple<int, int> getStartingHealth()
    {
        return Tuple.Create(casper.CurrentHealth, casper.MaxHealth);
    }

    public Tuple<int,int> getStartingAmmo()
    {
        return Tuple.Create(casper.CurrentAmmo, casper.MaxAmmo);
    }

    public void TestController()
    {
        if (Input.GetKeyDown("1"))
        {
            casper.changeHealth(1);
        }
        if (Input.GetKeyDown("2"))
        {
            casper.changeHealth(-1);
        }
        if (Input.GetKeyDown("3"))
        {
            casper.changeMaxHealth(1);
        }
        if (Input.GetKeyDown("4"))
        {
            casper.changeMaxHealth(-1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            casper.activateItem();
        }
        if (Input.GetKeyDown("5"))
        {
            SlowMotion.DoSlowMotion(5, 0.1f);
        }
    }
}
