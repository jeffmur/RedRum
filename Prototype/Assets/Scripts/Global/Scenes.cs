using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class Scenes
{
    private static string roomDimension = "4x4"; // Static demensions
    public static int currentLevel = 0;

    // Victory Case
    public static void Load(string path, string dimension, bool isCasperAlive)
    {
        roomDimension = dimension;
        Load(path, isCasperAlive);
        //displayMessage(dimensions);
    }

    // Losing/Retry case
    public static void Load(string path, bool isCasperAlive)
    {
        SceneManager.LoadScene(path);
        GlobalControl.Instance.ResetCasper(isCasperAlive);
    }

    public static void Load(string path)
    {
        SceneManager.LoadScene(path);
    }

    public static string getDimension()
    {
        return roomDimension;
    }

    public static int getInt()
    {
        return int.Parse(roomDimension[0].ToString());
    }

    public static string nextDimension()
    {
        switch (roomDimension[0])
        {
            case '4':
                return "5x5";
            case '5':
                return "6x6";
            default:
                return "4x4";
        }
    }

    public static Tuple<int, int> getDifficulty()
    {
        switch (currentLevel)
        {
            default:
                return Tuple.Create(2, 4); // Level 1
            case 2:
                return Tuple.Create(2, 5); // Level 2
            case 3:
                return Tuple.Create(3, 6); // Level 3
        }
    }

    public static void displayMessage(string msg)
    {
        EventManager.Instance.TriggerNotification(msg);
    }
}
