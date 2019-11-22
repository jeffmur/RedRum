using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public GameObject Panel;
    private void Start()
    {
        Panel.gameObject.SetActive(false); 
    }
    public void showHidePanel()
    {
        Panel.gameObject.SetActive(true);
    }
}
