using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    public Text notificationText;
    
    public IEnumerator displayMessage(string notification)
    {
        print(notification);
        notificationText.text = notification;
        notificationText.enabled = true;
        yield return new WaitForSeconds(2);
        notificationText.enabled = false;
    }
}
