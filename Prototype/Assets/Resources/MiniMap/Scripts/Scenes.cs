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
    }

    public static string getDem()
    {
        return DEM;
    }

    public static void setDem(string demensions)
    {
        DEM = demensions;
    }
}
