using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    private string Demensions;
    private char dem;
    public GameObject startPos;
    // ASSUMPTION: Starting from (-200,5,0)
    // Starts for 4x4
    public float minX = -205;
    public float maxX = -180;
    public float minY = -25;
    public float maxY = 5;
    /* Current Options for Demensions
     * 4x4 (16 rooms)
     * 5x5 (25 rooms)
     * 6x6 (36 rooms)
     */
    public void setUp(string DEMENSIONS)
    {
        if (DEMENSIONS != null)
        {
            Demensions = DEMENSIONS;
            dem = DEMENSIONS[0];
        } else
        {
            Demensions = "4x4";
            dem = '4';
        }
        disableLarger();
        switch (dem)
        {
            case '5':
                maxX = -170;
                minY = -35;
                break;
            case '6':
                maxX = -160;
                minY = -45;
                break;
            default:
                break;
        }
    }

    private void disableLarger()
    {
        // Hide all demensions > desired
        foreach(Transform child in startPos.transform)
            if (child.name[0] > dem)
                child.gameObject.SetActive(false);

        // Disable all borders who's name does not match
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Borders");
        foreach (GameObject size in borders)
            if (size.name != Demensions)
                size.SetActive(false);
    }
}
