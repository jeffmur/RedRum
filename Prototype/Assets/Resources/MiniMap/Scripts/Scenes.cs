using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class Scenes
{
    private static string DEM = "4x4"; // Static demensions

    public static void Load(string sceneName, string demensions)
    {
        DEM = demensions;
        SceneManager.LoadScene(sceneName);
        displayMessage(demensions);
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
