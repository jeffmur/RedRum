using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Info;
    public GameObject Main;
    public GameObject Controls;

    public void info()
    {
        Info.SetActive(true);
        Main.SetActive(false);
    }

    public void controls()
    {
        Controls.SetActive(true);
        Main.SetActive(false);
    }

    public void backToMenu()
    {
        Info.SetActive(false);
        Controls.SetActive(false);
        Main.SetActive(true);
    }

}
