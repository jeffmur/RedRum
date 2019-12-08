using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static string[] GAME = { "Level1", "Level2", "Level1", "Level1", "Win" };
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
    public void initGame()
    {
        Scenes.Load(GAME[0]);
    }
    public void retryLevel()
    {
        Scenes.Load(GAME[Scenes.currentLevel], Scenes.getDimension(), false);
    }

    public void nextLevel()
    {
        Scenes.currentLevel++;
        if (GAME[Scenes.currentLevel] != "Level2")
            Scenes.Load(GAME[Scenes.currentLevel], Scenes.nextDimension(), true);
        else
            Scenes.Load(GAME[Scenes.currentLevel], Scenes.getDimension(), true);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
