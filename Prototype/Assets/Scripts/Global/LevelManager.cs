using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    string GAME = "Beta";
    public static void Complete()
    {
        Cursor.visible = true;
        Scenes.Load("Complete", true);
    }
    public static void Dead()
    {
        Cursor.visible = true;
        Scenes.Load("Dead", false);
    }

    public void retryLevel()
    {
        Scenes.Load(GAME, Scenes.getDem(), false);
    }

    public void initGame()
    {
        Scenes.Load(GAME);
    }

    public void nextLevel()
    {
        Scenes.Load(GAME, Scenes.nextDem(), true);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
