using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemHover : MonoBehaviour
{
    public Image src;
    public Text Desc;
    private Toggle toggle;
    public Image bg;

    private void Start()
    {
        Desc.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }
    public void WhoAmI()
    {
        toggle = GetComponent<Toggle>();
        if (!src || !Desc)
        {
            Debug.LogError("please assign both the label and the toggle in the inspector");
            return;
        }
        Desc.gameObject.SetActive(true);
        bg.gameObject.SetActive(true);
    }

    public void NotMe()
    {
        Desc.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }
}
