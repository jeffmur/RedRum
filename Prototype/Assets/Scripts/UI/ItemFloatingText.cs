using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloatingText : MonoBehaviour
{
    public TMPro.TextMeshPro text;

    public void showFloatingText(Vector3 itemPosition, string message)
    {       
        gameObject.SetActive(true);
        Vector3 newLoc = new Vector3(itemPosition.x, itemPosition.y + 1, itemPosition.z);
        gameObject.transform.position = newLoc;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        text.GetComponent<TMPro.TextMeshPro>().text = message;
    }
}
