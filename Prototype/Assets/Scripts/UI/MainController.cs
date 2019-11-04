using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    //declare all UI elements and the model
    public UIHealth healthInfo;
    public NotificationUI notificationPanel;
    public GameWorld gameWorld;

   

    // Start is called before the first frame update
    void Start()
    {
        healthInfo.setStartingHealth(gameWorld.getStartingHealth());
        GameWorld.onNotifyChange += sendNotification;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void sendNotification(string notification)
    {
        StartCoroutine(notificationPanel.displayMessage(notification));
    }
}
