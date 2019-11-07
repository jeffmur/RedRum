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
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
