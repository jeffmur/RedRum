using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject Info;
    public GameObject MainMenu;
    public GameObject Controls;

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
        Scenes.Load("Alpha", Scenes.getDem(), false);
    }

    public void initGame()
    {
        Scenes.Load("Alpha");
    }

    public void nextLevel()
    {
        Scenes.Load("Alpha", Scenes.nextDem(), true);
    }

    public void info()
    {
        Info.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void controls()
    {
        Controls.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void backToMenu()
    {
        Info.SetActive(false);
        Controls.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
