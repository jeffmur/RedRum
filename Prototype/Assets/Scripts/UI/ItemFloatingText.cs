using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloatingText : MonoBehaviour
{
    public TMPro.TextMeshPro text;

    public void showFloatingText(Vector3 itemPosition, string message)
    {       
        gameObject.SetActive(true);
        gameObject.transform.position = itemPosition;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        text.GetComponent<TMPro.TextMeshPro>().text = message;
    }
}
