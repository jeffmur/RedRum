using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Change to demo scene for alpha Testing
        // Testing Level Generation
        //if (Input.GetKeyDown("4"))
        //    Scenes.Load("Alpha", "4x4");
        //if (Input.GetKeyDown("5"))
        //    Scenes.Load("Alpha", "5x5");
        //if (Input.GetKeyDown("6"))
        //    Scenes.Load("Alpha", "6x6");
    }

    public static void Complete()
    {
        Cursor.visible = true;
        Scenes.Load("Complete", true);
    }

    public void nextLevel()
    {
        Scenes.Load("Alpha", Scenes.nextDem(), true);
    }

    public void Fail()
    {

    }

    private void retry()
    {

    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
