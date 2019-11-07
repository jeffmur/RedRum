using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameWorld : MonoBehaviour
{
    private PlayerStats stats;

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
            stats.incrementMaxHeath();
        }
        if (Input.GetKeyDown("2"))
        {
            stats.decrementMaxHeath();
        }
    }

    public delegate void onNotifyDelegate(string notification);
    public static event onNotifyDelegate onNotifyChange;

    public static void triggerNotification(string notification)
    {
        onNotifyChange?.Invoke(notification);
    }
}
