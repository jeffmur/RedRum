using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static string[] GAME = { "Level1", "Level2" };
    int Level = 0;
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
        Scenes.Load(GAME[Level], Scenes.getDimension(), false);
    }

    public void nextLevel()
    {
        Level++;
        Scenes.Load(GAME[Level], Scenes.nextDimension(), true);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
