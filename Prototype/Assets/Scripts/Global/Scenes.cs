using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class Scenes
{
    private static string DEM = "4x4"; // Static demensions

    // Victory Case
    public static void Load(string path, string demensions, bool isCasperAlive)
    {
        DEM = demensions;
        Load(path, isCasperAlive);
        //displayMessage(demensions);
    }

    // Losing/Retry case
    public static void Load(string path, bool isCasperAlive)
    {
        if(!isCasperAlive) { GlobalControl.Instance.ResetCasper(); }
        SceneManager.LoadScene(path);
    }

    public static string getDem()
    {
        return DEM;
    }

    public static string nextDem()
    {
        switch (DEM[0])
        {
            case '4':
                return "5x5";
            case '5':
                return "6x6";
            default:
                return "4x4";
        }
    }

    public static void displayMessage(string msg)
    {
        EventManager.TriggerNotification(msg);
    }
}
