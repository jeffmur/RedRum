using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    public string Demensions;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUp(string DEMENSIONS)
    {
        Demensions = DEMENSIONS;
        dem = DEMENSIONS[0];
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
