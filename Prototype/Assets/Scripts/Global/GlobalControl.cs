using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public CasperData savedCasperData = new CasperData();
    public PlayerData savedPlayerData = new PlayerData();

    void Awake()
    {
        // Carry Stat data over scenes
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Debug.Log("DONT");
        }      
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("DESTROY "+gameObject.name);
        }
    }

    public void ResetCasper()
    {
        savedCasperData = new CasperData();
    }

    public void saveItem(GameObject obj)
    {
        DontDestroyOnLoad(obj);
    }
}
